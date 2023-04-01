using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class Particle
    {
        public int name;
        public Vector2 position;
        public float rotation;
        public Vector2 direction;
        public float speed;
        public TimeSpan lifetime;

        public Particle(int name, Vector2 position, Vector2 direction, float speed, TimeSpan lifetime)
        {
            if (direction.Y < 0)
            {
                direction.Y = 0;
            }
            if (speed < 0)
            {
                speed = 0.1f;
            }

            this.name = name;
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.lifetime = lifetime;

            this.rotation = 0;
        }
    }
}
