
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace BigBlue
{
    public class Animation
    {
        public float FrameTime { get; set; }
        public int CurrentFrame { get; set; }
        public float ElapsedTime { get; set; }
        public Texture2D[] Frames { get; private set; }

        public Animation(Texture2D[] frames)
        {
            Frames = frames;
            FrameTime = 1000f / (float)frames.Length;
            CurrentFrame = 0;
            ElapsedTime = 0f;
        }

    }
}
