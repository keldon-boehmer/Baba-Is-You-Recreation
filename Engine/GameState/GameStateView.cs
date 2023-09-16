using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Baba
{
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        public abstract void loadContent(ContentManager contentManager);
        public abstract GameStateEnum processInput(GameTime gameTime);
        public abstract void render(GameTime gameTime);
        public abstract void update(GameTime gameTime);
    }
}
