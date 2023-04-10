using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue
{

    public class LevelSelectView : GameStateView
    {
        private SpriteFont fontMenu;
        private SpriteFont fontMenuSelect;
        private bool waitForKeyRelease = true;

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
                    waitForKeyRelease = true;
                    LevelManager.Instance.addCurrentLevelIndex();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    waitForKeyRelease = true;
                    LevelManager.Instance.subtractCurrentLevelIndex();
                }

                // If enter is pressed, return the appropriate new state
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    waitForKeyRelease = true;
                    return GameStateEnum.GamePlay;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    waitForKeyRelease = true;
                    return GameStateEnum.MainMenu;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                waitForKeyRelease = false;
            }
            return GameStateEnum.LevelSelect;
        }

        public override void update(GameTime gameTime)
        {
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            float bottom = graphics.PreferredBackBufferHeight / 100;

            for (int i = 0; i < LevelManager.Instance.LevelCount; i++)
            {
                SpriteFont textFont = fontMenu;
                string text = LevelManager.Instance.getLevelName(i);
                Color textColor = Color.Blue;

                if (LevelManager.Instance.equalsCurrentLevelIndex(i))
                {
                    textColor = Color.Yellow;
                    textFont = fontMenuSelect;
                }
                bottom = drawMenuItem(textFont, text, bottom, textColor);
            }

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
