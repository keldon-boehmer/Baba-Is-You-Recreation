using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BigBlue.ECS
{
    internal class Animation
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

        private Animation(int frameTime, int currentFrame, int elapsedTime, Texture2D[] frames, Color color)
        {
            FrameTime = frameTime;
            CurrentFrame = currentFrame;
            ElapsedTime = elapsedTime;
            Frames = frames;
            Color = color;
        }

        public Animation Clone()
        {
            return new Animation(FrameTime, CurrentFrame, ElapsedTime, Frames, Color);
        }
    }
}
