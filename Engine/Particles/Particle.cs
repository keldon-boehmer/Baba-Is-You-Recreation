using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBlue
{
    public class Particle
    {
        public Vector2 position;
        public float rotation;
        public Vector2 direction;
        public float speed;
        public TimeSpan lifetime;
        public int width;
        public int height;
        public Color color;

        public Particle(Vector2 position, Vector2 direction, float speed, TimeSpan lifetime, int width, int height, Color color)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.lifetime = lifetime;
            this.width = width;
            this.height = height;
            this.color = color;

            rotation = 0;
        }
    }
}
