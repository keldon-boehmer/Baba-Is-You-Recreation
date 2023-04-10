using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBlue
{
    public class Animation
    {
        public int FrameTime { get; }
        public int CurrentFrame { get; set; }
        public int ElapsedTime { get; set; }
        public Texture2D[] Frames { get; private set; }
        public Color Color;

        public Animation(Texture2D[] frames, Color color)
        {
            Frames = frames;
            FrameTime = 1000 / frames.Length;
            CurrentFrame = 0;
            ElapsedTime = 0;
            Color = color;
        }

    }
}
