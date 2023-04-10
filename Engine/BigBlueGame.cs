using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
            createLevels();

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
            }

            currentState.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentState.render(gameTime);

            currentState = states[nextStateEnum];

            base.Draw(gameTime);
        }

        // TODO : Make this method parse the input levels file to create levels. Alternatively, implement the method in LevelManager and call it in this file's Initialize method.
        private void createLevels()
        {
            LevelManager.Instance.addLevel(new Level("Level 1", 10, 10));
            LevelManager.Instance.addLevel(new Level("Level 2", 10, 10));
            LevelManager.Instance.addLevel(new Level("Level 3", 10, 10));
        }
    }
}