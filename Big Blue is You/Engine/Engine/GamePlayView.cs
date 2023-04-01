using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;

namespace Engine
{
    public class GamePlayView : GameStateView
    {
        private SpriteFont font;
        private SpriteFont fontSelect;
        private Song music;

        private int screenWidth;
        private int screenHeight;

        private bool paused = false;

        private bool gameOver = false;

        private bool countdown3 = true;
        private bool countdown2 = false;
        private bool countdown1 = false;
        private bool countdownGo = false;
        private bool gameStarted = false;
        private bool musicStarted = false;

        private double countdownTimer = 0;

        private int score = 0;

        private Texture2D particleTex;

        private Rectangle livesBox;
        private Texture2D lives;

        private Rectangle backgroundBox;
        private Texture2D background;

        HighScores topScores;
        SaveManager saveManager = new SaveManager();

        private enum PauseState
        {
            Resume,
            Exit
        }
        private PauseState currentSelection = PauseState.Resume;
        private bool waitForKeyRelease = true;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            MediaPlayer.IsRepeating = true;

            topScores = saveManager.loadScoresFromFile();

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            livesBox = new Rectangle(0, 93 * screenHeight / 100, screenWidth / 15, screenHeight / 15);
            backgroundBox = new Rectangle(0, 0, screenWidth, screenHeight);

        }

        public override void loadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("Fonts/menu");
            fontSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");

            //music = contentManager.Load<Song>("Music/music");

            //lives = contentManager.Load<Texture2D>("Textures/remain");

            //background = contentManager.Load<Texture2D>("Textures/background");

            //particleTex = contentManager.Load<Texture2D>("Textures/particle");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (paused)
            {
                return processPauseInput();
            }
            else if (gameOver)
            {
                return processGameOverInput();
            }
            else
            {
                if (gameStarted)
                {
                    processPlayInput(gameTime);
                }
                return GameStateEnum.GamePlay;
            }

        }
        public override void update(GameTime gameTime)
        {
            if (paused || gameOver)
            {
                return;
            }
            if (gameStarted)
            {
                if (!musicStarted)
                {
                    //MediaPlayer.Play(music);
                    musicStarted = true;
                }
                updateGameplay(gameTime);
            }
            else if (countdownGo)
            {
                updateCountdownGo(gameTime);
            }
            else if (countdown1)
            {
                updateCountdown1(gameTime);
            }
            else if (countdown2)
            {
                updateCountdown2(gameTime);
            }
            else if (countdown3)
            {
                updateCountdown3(gameTime);
            }
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (paused)
            {
                renderPauseMenu();
            }
            else if (gameOver)
            {
                renderGameOver();
            }
            else
            {
                if (gameStarted)
                {
                    renderPlay();
                }
                else if (countdownGo)
                {
                    renderCountdown("Go!");
                }
                else if (countdown1)
                {
                    renderCountdown("1");
                }
                else if (countdown2)
                {
                    renderCountdown("2");
                }
                else if (countdown3)
                {
                    renderCountdown("3");
                }
            }

            spriteBatch.End();
        }

        #region Countdown

        private void updateCountdownGo(GameTime gameTime)
        {
            countdownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countdownTimer > 1000)
            {
                countdownTimer = 0;
                countdownGo = false;
                gameStarted = true;
            }
        }

        private void updateCountdown1(GameTime gameTime)
        {
            countdownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countdownTimer > 1000)
            {
                countdownTimer = 0;
                countdown1 = false;
                countdownGo = true;
            }
        }

        private void updateCountdown2(GameTime gameTime)
        {
            countdownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countdownTimer > 1000)
            {
                countdownTimer = 0;
                countdown2 = false;
                countdown1 = true;
            }
        }

        private void updateCountdown3(GameTime gameTime)
        {
            countdownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countdownTimer > 1000)
            {
                countdownTimer = 0;
                countdown3 = false;
                countdown2 = true;
            }
        }

        private void renderCountdown(string text)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                fontSelect,
                text,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, 500),
                Color.Black);
        }

        #endregion

        #region Gameplay
        private void processPlayInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
            }
            else
            {
                // process gameplay input here
            }
            
        }

        private void updateGameplay(GameTime gameTime)
        {
            //updateParticleEmitters(gameTime);
        }

        /*private void updateParticleEmitters(GameTime gameTime)
        {
            for (int i = 0; i < board.bricks.GetLength(0); i++)
            {
                for (int j = 0; j < board.bricks.GetLength(1); j++)
                {
                    Brick brick = board.bricks[i, j];
                    if (brick.systemActive)
                    {
                        brick.systemActive = brick.system.update(gameTime);
                    }
                }
            }
        }*/

        private void renderPlay()
        {
            //renderBackground();
            renderScore();
        }

        private void renderBackground()
        {
            spriteBatch.Draw(background, backgroundBox, Color.White);
        }

        private void renderScore()
        {
            string scoreString = "Score: " + score;
            Vector2 stringSize = font.MeasureString(scoreString);
            spriteBatch.DrawString(font, scoreString,
                new Vector2(screenWidth - stringSize.X * 3 / 2, 96 * screenHeight / 100 - stringSize.Y / 2), Color.Yellow);
        }

        #endregion

        #region Pause Menu
        private GameStateEnum processPauseInput()
        {
            if (!waitForKeyRelease)
            {
                // Arrow keys to navigate the menu
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if ((int)currentSelection == 0)
                    {
                        currentSelection++;
                    }
                    waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if ((int)currentSelection == 1)
                    {
                        currentSelection--;
                    }
                    waitForKeyRelease = true;
                }

                // If enter is pressed, return the appropriate new state
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == PauseState.Resume)
                {
                    paused = false;
                    waitForKeyRelease = true;
                    return GameStateEnum.GamePlay;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == PauseState.Exit)
                {
                    waitForKeyRelease = true;
                    saveScore();
                    return GameStateEnum.Exit;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                waitForKeyRelease = false;
            }

            return GameStateEnum.GamePlay;
        }

        private void renderPauseMenu()
        {
            float bottom = drawPauseMenuItem(
                currentSelection == PauseState.Resume ? fontSelect : font,
                "Resume",
                400,
                currentSelection == PauseState.Resume ? Color.Yellow : Color.Blue);
            drawPauseMenuItem(currentSelection == PauseState.Exit ? fontSelect : font, "Exit Game", bottom, currentSelection == PauseState.Exit ? Color.Yellow : Color.Blue);
        }

        private float drawPauseMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                font,
                text,
                new Vector2(screenWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }
        #endregion

        #region Game Over Screen
        private GameStateEnum processGameOverInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.Exit;
            }
            return GameStateEnum.GamePlay;
        }

        private void renderGameOver()
        {
            string message = "GAME OVER!\nThank you for playing!\nYou received a score of " + score + "\nPress 'ESC' key to exit game";
            Vector2 stringSize = font.MeasureString(message);
            spriteBatch.DrawString(font, message, new Vector2(screenWidth / 2 - stringSize.X / 2, screenHeight / 2 - stringSize.Y / 2), Color.Black);
        }


        private void saveScore()
        {
            topScores.saveNewScore(score);
            saveManager.saveScoresToFile(topScores);
        }
        #endregion
    }
}
