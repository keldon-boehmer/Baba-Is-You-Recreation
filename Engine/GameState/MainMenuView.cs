using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace BigBlue
{
    public class MainMenuView : GameStateView
    {
        private SpriteFont fontMenu;
        private SpriteFont fontMenuSelect;

        private enum MenuState
        {
            NewGame,
            HighScores,
            About,
            Quit
        }

        private MenuState currentSelection = MenuState.NewGame;
        private bool waitForKeyRelease = false;

        public override void loadContent(ContentManager contentManager)
        {
            fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (!waitForKeyRelease)
            {
                // Arrow keys to navigate the menu
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if ((int)currentSelection < 3)
                    {
                        currentSelection++;
                    }
                    waitForKeyRelease = true;

                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if ((int)currentSelection > 0)
                    {
                        currentSelection--;
                    }
                    waitForKeyRelease = true;
                }

                // If enter is pressed, return the appropriate new state
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == MenuState.NewGame)
                {
                    waitForKeyRelease = true;
                    return GameStateEnum.GamePlay;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == MenuState.HighScores)
                {
                    waitForKeyRelease = true;
                    return GameStateEnum.Controls;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == MenuState.About)
                {
                    waitForKeyRelease = true;
                    return GameStateEnum.About;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentSelection == MenuState.Quit)
                {
                    return GameStateEnum.Exit;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                waitForKeyRelease = false;
            }

            return GameStateEnum.MainMenu;
        }

        public override void update(GameTime gameTime)
        {
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 stringSize = fontMenu.MeasureString("Big Blue is You!");
            spriteBatch.DrawString(
                fontMenu,
                "Big Blue is You!",
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 10),
                Color.Blue);

            float bottom = drawMenuItem(
                currentSelection == MenuState.NewGame ? fontMenuSelect : fontMenu, 
                "New Game",
                graphics.PreferredBackBufferHeight * 4 / 10, 
                currentSelection == MenuState.NewGame ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(currentSelection == MenuState.HighScores ? fontMenuSelect : fontMenu, "Controls", bottom, currentSelection == MenuState.HighScores ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(currentSelection == MenuState.About ? fontMenuSelect : fontMenu, "Credits", bottom, currentSelection == MenuState.About ? Color.Yellow : Color.Blue);
            drawMenuItem(currentSelection == MenuState.Quit ? fontMenuSelect : fontMenu, "Quit", bottom, currentSelection == MenuState.Quit ? Color.Yellow : Color.Blue);

            spriteBatch.End();
        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                font,
                text,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }
    }
}