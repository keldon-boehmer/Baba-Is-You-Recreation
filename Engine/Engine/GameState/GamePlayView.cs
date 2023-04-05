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

        private Texture2D[] bigBlueSheet;
        private Texture2D[] flagSheet;
        private Texture2D[] floorSheet;
        private Texture2D[] flowersSheet;
        private Texture2D[] grassSheet;
        private Texture2D[] hedgeSheet;
        private Texture2D[] lavaSheet;
        private Texture2D[] rockSheet;
        private Texture2D[] wallSheet;
        private Texture2D[] waterSheet;
        private Texture2D[] wordBabaSheet;
        private Texture2D[] wordFlagSheet;
        private Texture2D[] wordIsSheet;
        private Texture2D[] wordKillSheet;
        private Texture2D[] wordLavaSheet;
        private Texture2D[] wordPushSheet;
        private Texture2D[] wordRockSheet;
        private Texture2D[] wordSinkSheet;
        private Texture2D[] wordStopSheet;
        private Texture2D[] wordWallSheet;
        private Texture2D[] wordWaterSheet;
        private Texture2D[] wordWinSheet;
        private Texture2D[] wordYouSheet;


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

            Texture2D bigBlue = contentManager.Load<Texture2D>("Textures/BigBlue/BigBlue");
            
            bigBlueSheet = new Texture2D[] { bigBlue };
            flagSheet = createSpriteSheet(contentManager, "Flag", "flag");
            floorSheet = createSpriteSheet(contentManager, "Floor", "floor");
            flowersSheet = createSpriteSheet(contentManager, "Flowers", "flowers");
            grassSheet = createSpriteSheet(contentManager, "Grass", "grass");
            hedgeSheet = createSpriteSheet(contentManager, "Hedge", "hedge");
            lavaSheet = createSpriteSheet(contentManager, "Lava", "lava");
            rockSheet = createSpriteSheet(contentManager, "Rock", "rock");
            wallSheet = createSpriteSheet(contentManager, "Wall", "wall");
            waterSheet = createSpriteSheet(contentManager, "Water", "water");
            wordBabaSheet = createSpriteSheet(contentManager, "Word-Baba", "word-baba");
            wordFlagSheet = createSpriteSheet(contentManager, "Word-Flag", "word-flag");
            wordIsSheet = createSpriteSheet(contentManager, "Word-Is", "word-is");
            wordKillSheet = createSpriteSheet(contentManager, "Word-Kill", "word-kill");
            wordLavaSheet = createSpriteSheet(contentManager, "Word-Lava", "word-lava");
            wordPushSheet = createSpriteSheet(contentManager, "Word-Push", "word-push");
            wordRockSheet = createSpriteSheet(contentManager, "Word-Rock", "word-rock");
            wordSinkSheet = createSpriteSheet(contentManager, "Word-Sink", "word-sink");
            wordStopSheet = createSpriteSheet(contentManager, "Word-Stop", "word-stop");
            wordWallSheet = createSpriteSheet(contentManager, "Word-Wall", "word-wall");
            wordWaterSheet = createSpriteSheet(contentManager, "Word-Water", "word-water");
            wordWinSheet = createSpriteSheet(contentManager, "Word-Win", "word-win");
            wordYouSheet = createSpriteSheet(contentManager, "Word-You", "word-you");
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
            spriteBatch.Draw(rockSheet[1], new Rectangle(500, 500, 40, 40), Color.Brown);
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
                    waitForKeyRelease = true;
                    return GameStateEnum.MainMenu;
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

        #region Content Load

        private Texture2D[] createSpriteSheet(ContentManager contentManager, string parentFolder, string objectName)
        {
            string name0 = $"Textures/{parentFolder}/{objectName}_0";
            string name1 = $"Textures/{parentFolder}/{objectName}_1";
            string name2 = $"Textures/{parentFolder}/{objectName}_2";

            Texture2D sprite0 = contentManager.Load<Texture2D>(name0);
            Texture2D sprite1 = contentManager.Load<Texture2D>(name1);
            Texture2D sprite2 = contentManager.Load<Texture2D>(name2);

            Texture2D[] spriteSheet = new Texture2D[] {sprite0, sprite1, sprite2};
            return spriteSheet;
        }

        #endregion

    }
}
