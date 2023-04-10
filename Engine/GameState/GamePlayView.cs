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
using Microsoft.Xna.Framework.Audio;
using System.Xml.Linq;

namespace BigBlue
{
    public class GamePlayView : GameStateView
    {
        private SpriteFont font;
        private SpriteFont fontSelect;
        private Song music;
        private SoundEffect moveEffect;
        private SoundEffect onVictoryEffect;
        private SoundEffect onIsWinConditionChangeEffect;


        private int screenWidth;
        private int screenHeight;

        private bool paused = false;

        private bool gameStarted = true;
        private bool musicStarted = false;

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
            MediaPlayer.Volume = 0.2f;

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            ParticleSystem.setBlankTexture(graphicsDevice);

        }

        public override void loadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("Fonts/menu");
            fontSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");

            music = contentManager.Load<Song>("Sound/music");
            moveEffect = contentManager.Load<SoundEffect>("Sound/move");
            onVictoryEffect = contentManager.Load<SoundEffect>("Sound/victory");
            onIsWinConditionChangeEffect = contentManager.Load<SoundEffect>("Sound/winConditionChange");

            WorldCreator.InitializeSheets(contentManager);
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (paused)
            {
                return processPauseInput();
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
            if (paused)
            {
                return;
            }
            if (gameStarted)
            {
                
                if (!musicStarted)
                {
                    MediaPlayer.Play(music);
                    musicStarted = true;
                }
                updateGameplay(gameTime);
            }

            InputManager.Instance.ResetInputs();
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            if (paused)
            {
                renderPauseMenu();
            }
            else
            {
                if (gameStarted)
                {
                    renderPlay();
                }
            }

            spriteBatch.End();
        }

        #region Gameplay
        private void processPlayInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
            }
            else
            {
                InputManager.Instance.ProcessInput();
            }
            
        }

        private void updateGameplay(GameTime gameTime)
        {

            ParticleSystem.update(gameTime);

            if (InputManager.Instance.moveDown)
            {
                moveEffect.Play();
                ParticleSystem.OnDeath(new Rectangle(100, 100, 40, 40), 50, 2f, new TimeSpan(0, 0, 0, 0, 3000), Color.Yellow);
            }
            if (InputManager.Instance.moveLeft)
            {
                onIsWinConditionChangeEffect.Play();
                ParticleSystem.IsWinOrIsYou(new Rectangle(100, 100, 40, 40), 10, 2f, new TimeSpan(0, 0, 0, 0, 3000), Color.Yellow);
            }
            if (InputManager.Instance.moveRight)
            {
                onVictoryEffect.Play();
                ParticleSystem.PlayerIsWin(1, 500, 5f, new TimeSpan(0, 0, 0, 0, 3000), new Vector2(screenWidth, screenHeight));
            }
        }

        private void renderPlay()
        {
            ParticleSystem.draw(spriteBatch);
        }

        #endregion

        #region Pause Menu
        private GameStateEnum processPauseInput()
        {
            if (!waitForKeyRelease)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if ((int)currentSelection == 0)
                    {
                        currentSelection++;
                    }
                    waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
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
                    paused = false;
                    currentSelection = 0;
                    ParticleSystem.ClearParticles();
                    waitForKeyRelease = true;
                    return GameStateEnum.LevelSelect;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Enter))
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
                screenHeight * 4 / 10,
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

    }
}
