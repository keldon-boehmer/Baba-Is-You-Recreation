using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue
{
    public class ParticleSystem
    {

        private Dictionary<int, Particle> particles = new Dictionary<int, Particle>();
        private Texture2D particleTex;
        private MyRandom random = new MyRandom();

        private int sourceX;
        private int sourceY;
        private int particleSize;
        private int speed;
        private TimeSpan lifetime;
        private int width;
        private int height;

        public Vector2 Gravity { get; set; }

        public ParticleSystem(Texture2D texture, int sourceX, int sourceY, int size, int speed, TimeSpan lifetime, int width, int height)
        {
            this.sourceX = sourceX;
            this.sourceY = sourceY;
            particleSize = size;
            this.speed = speed;
            this.lifetime = lifetime;
            this.width = width;
            this.height = height;

            particleTex = texture;

            this.Gravity = new Vector2(0, 0.1F);

            for (int i = 0; i < 100; i++)
            {
                float randomX = random.nextRange((float)sourceX, (float)(sourceX + width));
                float randomY = random.nextRange((float)sourceY, (float)(sourceY + height));
                Vector2 direction = random.nextCircleVector();
                direction.X = 0;
                Particle p = new Particle(
                    random.Next(),
                    new Vector2((int)randomX, (int)randomY),
                    direction,
                    (float)random.nextGaussian(this.speed, 1),
                    this.lifetime);

                if (!particles.ContainsKey(p.name))
                {
                    particles.Add(p.name, p);
                }
            }
        }
        public int ParticleCount
        {
            get { return particles.Count; }
        }

        /// <summary>
        /// Generates new particles, updates the state of existing ones and retires expired particles.
        /// </summary>
        public bool update(GameTime gameTime)
        {
            //
            // For any existing particles, update them, if we find ones that have expired, add them
            // to the remove list.
            List<int> removeMe = new List<int>();
            foreach (Particle p in particles.Values)
            {
                p.lifetime -= gameTime.ElapsedGameTime;
                if (p.lifetime < TimeSpan.Zero)
                {
                    //
                    // Add to the remove list
                    removeMe.Add(p.name);
                }
                //
                // Update its position
                p.position += (p.direction * p.speed);
                //
                // Have it rotate proportional to its speed
                p.rotation += p.speed / 50.0f;
                //
                // Apply some gravity
                p.direction += this.Gravity;
            }

            //
            // Remove any expired particles
            foreach (int Key in removeMe)
            {
                particles.Remove(Key);
            }

            return particles.Count > 0;
        }

        /// <summary>
        /// Renders the active particles
        /// </summary>
        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(0, 0, particleSize, particleSize);
            foreach (Particle p in particles.Values)
            {
                Texture2D texDraw = particleTex;

                r.X = (int)p.position.X;
                r.Y = (int)p.position.Y;
                spriteBatch.Draw(
                    texDraw,
                    r,
                    null,
                    Color.White,
                    p.rotation,
                    new Vector2(texDraw.Width / 2, texDraw.Height / 2),
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
