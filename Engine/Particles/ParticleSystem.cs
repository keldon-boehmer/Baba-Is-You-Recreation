using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Baba
{
    public static class ParticleSystem
    {
        private static List<Particle> _particles = new List<Particle>();
        private static Texture2D _blankTexture;

        #region Particle Creation Methods

        // This is the sparkle/highlight effect when the condition for IsYou or IsWin changes.
        public static void IsWinOrIsYou(Rectangle rectangle, int numberOfParticles, float particleSpeed, TimeSpan lifetime, Color color)
        {
            // Calculate the positions of the four corners of the rectangle
            Vector2 topLeft = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 topRight = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 bottomLeft = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 bottomRight = new Vector2(rectangle.Right, rectangle.Bottom);

            // Calculate the size of the particles
            int particleWidth = rectangle.Width / numberOfParticles;
            int particleHeight = rectangle.Height / numberOfParticles;

            // Create particles along each of the four sides of the rectangle, directed away from the center of the rectangle
            CreateParticlesAlongLine(topLeft, topRight, numberOfParticles, particleWidth, particleHeight, particleSpeed, lifetime, color);
            CreateParticlesAlongLine(topRight, bottomRight, numberOfParticles, particleWidth, particleHeight, particleSpeed, lifetime, color);
            CreateParticlesAlongLine(bottomRight, bottomLeft, numberOfParticles, particleWidth, particleHeight, particleSpeed, lifetime,color);
            CreateParticlesAlongLine(bottomLeft, topLeft, numberOfParticles, particleWidth, particleHeight, particleSpeed, lifetime, color);
        }

        private static void CreateParticlesAlongLine(Vector2 start, Vector2 end, int numberOfParticles, int particleWidth, int particleHeight, float particleSpeed, TimeSpan lifetime, Color color)
        {
            // Calculate the length and direction of the line segment
            Vector2 direction = end - start;
            float length = direction.Length();
            direction.Normalize();

            // Calculate the distance between particles
            float distanceBetweenParticles = length / numberOfParticles;

            // Loop through the specified number of particles, creating each particle at an endpoint of an equal segment along the line segment and directed away from the center of the rectangle
            for (int i = 0; i < numberOfParticles; i++)
            {
                // Calculate the position of the particle at the endpoint of an equal segment along the line segment
                float positionAlongLine = i * distanceBetweenParticles;
                Vector2 position = start + positionAlongLine * direction;

                // Create a particle with no direction
                Vector2 particleDirection = new Vector2(0, 0);

                Particle particle = new Particle(position, particleDirection, particleSpeed, lifetime, particleWidth, particleHeight, color);
                _particles.Add(particle);
            }
        }

        // This is the particle explosion that occurs when an object dies
        public static void OnDeath(Rectangle rectangle, int numberOfParticles, float particleSpeed, TimeSpan lifetime, Color color)
        {
            Random random = new Random();

            int centerX = rectangle.X + rectangle.Width / 2;
            int centerY = rectangle.Y + rectangle.Height / 2;
            int particleWidth = rectangle.Width / 20;
            int particleHeight = particleWidth;

            for (int i = 0; i < numberOfParticles; i++)
            {
                float angle = MathHelper.ToRadians(random.Next(360));
                float speed = (float)random.NextDouble() * particleSpeed;
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                Particle particle = new Particle(new Vector2(centerX, centerY), direction, speed, lifetime, particleWidth, particleHeight, color);

                _particles.Add(particle);
            }
        }

        // This is the fireworks effect that will occur when the player achieves victory.
        public static void PlayerIsWin(int numberOfFireworks, int particlesPerFirework, float particleSpeed, TimeSpan lifetime, Vector2 screenDimensions)
        {
            Random random = new Random();

            float yCoord = screenDimensions.Y / 2f;
            int particleWidth = (int)(screenDimensions.X / 500);
            int particleHeight = (int)(screenDimensions.Y / 250);
            List<Color> colorOptions = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Yellow };

            // Calculate the distance between each firework
            float xSpacing = screenDimensions.X / numberOfFireworks;

            // Create the specified number of fireworks at evenly spaced positions along the x-axis
            for (int i = 0; i < numberOfFireworks; i++)
            {
                // Calculate the x-coordinate for this firework
                float xCoord = (i + 0.5f) * xSpacing;
                Vector2 position = new Vector2(xCoord, yCoord);

                // Create the particles for the firework
                for (int j = 0; j < particlesPerFirework; j++)
                {
                    // Calculate a random direction for the particle
                    float particleAngle = (float)random.NextDouble() * MathHelper.TwoPi;
                    Vector2 particleDirection = new Vector2((float)Math.Cos(particleAngle), (float)Math.Sin(particleAngle));

                    // Create the particle with a random speed, color
                    float thisParticleSpeed = (float)random.NextDouble() * particleSpeed;
                    Color particleColor = colorOptions[random.Next(colorOptions.Count)];
                    Particle particle = new Particle(position, particleDirection, thisParticleSpeed, lifetime, particleWidth, particleHeight, particleColor);

                    // Add the particle to the list of particles
                    _particles.Add(particle);
                }
            }
        }
        #endregion


        public static bool update(GameTime gameTime)
        {

            List<Particle> removeMe = new List<Particle>();
            foreach (Particle p in _particles)
            {
                p.lifetime -= gameTime.ElapsedGameTime;
                if (p.lifetime < TimeSpan.Zero)
                {
                    //
                    // Add to the remove list
                    removeMe.Add(p);
                }
                //
                // Update its position
                p.position += p.direction * p.speed;
                //
                // Have it rotate proportional to its speed
                p.rotation += p.speed / 10.0f;
            }

            //
            // Remove any expired particles
            foreach (Particle p in removeMe)
            {
                _particles.Remove(p);
            }

            return _particles.Count > 0;
        }

        /// <summary>
        /// Renders the active _particles
        /// </summary>
        public static void draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(0, 0, 0, 0);
            foreach (Particle p in _particles)
            {
                r.X = (int)p.position.X;
                r.Y = (int)p.position.Y;
                r.Width = p.width;
                r.Height = p.height;
                spriteBatch.Draw(
                    _blankTexture,
                    r,
                    null,
                    p.color,
                    p.rotation,
                    new Vector2(_blankTexture.Width / 2, _blankTexture.Height / 2),
                    SpriteEffects.None,
                    0);
            }
        }

        public static void setBlankTexture(GraphicsDevice graphics)
        {
            _blankTexture = new Texture2D(graphics, 1, 1);
            _blankTexture.SetData(new Color[] { Color.White });
        }

        public static void ClearParticles()
        {
            _particles.Clear();
        }
    }
}
