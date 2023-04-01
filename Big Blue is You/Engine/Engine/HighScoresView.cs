using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Threading;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Serialization;

namespace Engine
{
    public class HighScoresView : GameStateView
    {
        private SpriteFont font;
        private const string MESSAGE1 = "HIGH SCORES";
        private const string MESSAGE2 = "Press 'R' key to reset scores (must restart game to take effect)";

        private HighScores topScores;
        private SaveManager saveManager = new SaveManager();
        private int score1;
        private int score2;
        private int score3;
        private int score4;
        private int score5;

        KeyboardState prevKeyState;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);

            prevKeyState = Keyboard.GetState();
            topScores = saveManager.loadScoresFromFile();
            score1 = topScores.scores[0];
            score2 = topScores.scores[1];
            score3 = topScores.scores[2];
            score4 = topScores.scores[3];
            score5 = topScores.scores[4];
        }

        public override void loadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            {
                prevKeyState = Keyboard.GetState();
                return GameStateEnum.MainMenu;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                prevKeyState = Keyboard.GetState();
                topScores.resetScores();
                saveManager.saveScoresToFile(topScores);
                return GameStateEnum.HighScores;
            }
            prevKeyState = Keyboard.GetState();
            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 stringSize = font.MeasureString(MESSAGE1);
            spriteBatch.DrawString(font, MESSAGE1,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 10 - stringSize.Y), Color.Yellow);
            stringSize = font.MeasureString(MESSAGE2);
            spriteBatch.DrawString(font, MESSAGE2,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, graphics.PreferredBackBufferHeight / 5 - stringSize.Y), Color.Yellow);

            float startY = drawScore(font, "" + score1, 400);
            startY = drawScore(font, "" + score2, startY);
            startY = drawScore(font, "" + score3, startY);
            startY = drawScore(font, "" + score4, startY);
            drawScore(font, "" + score5, startY);
            spriteBatch.End();
        }

        private float drawScore(SpriteFont font, string text, float y)
        {
            Vector2 stringSize = font.MeasureString(text);
            spriteBatch.DrawString(
                font,
                text,
                new Vector2(graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                Color.Black);

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
