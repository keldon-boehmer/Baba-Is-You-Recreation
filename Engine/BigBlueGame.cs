using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace BigBlue
{
    public class BigBlueGame : Game
    {
        private GraphicsDeviceManager graphics;
        private IGameState currentState;
        private GameStateEnum nextStateEnum = GameStateEnum.MainMenu;
        private Dictionary<GameStateEnum, IGameState> states;

        public BigBlueGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            // Create levels
            LevelManager.Instance.CreateAllLevels();

            // Create all game states
            states = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.LevelSelect, new LevelSelectView() },
                { GameStateEnum.GamePlay, new GamePlayView() },
                { GameStateEnum.Controls, new ControlsView() },
                { GameStateEnum.About, new AboutView() }
            };

            // Give all game states a chance to initialize, other than constructor
            foreach (var item in states)
            {
                item.Value.initialize(this.GraphicsDevice, graphics);
            }

            // We are starting with the main menu - as defined by the value set in nextStateEnum
            currentState = states[nextStateEnum];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Give all game states a chance to load their content
            foreach (var item in states)
            {
                item.Value.loadContent(this.Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            nextStateEnum = currentState.processInput(gameTime);
            // Special case for exiting the game
            if (nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
                return;
            }

            currentState.update(gameTime);
            currentState = states[nextStateEnum];

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            currentState.render(gameTime);

            base.Draw(gameTime);
        }
    }
}