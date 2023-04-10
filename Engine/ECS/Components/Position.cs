using Microsoft.Xna.Framework;

namespace BigBlue
{
    public class Position
    {
        public Vector2 _position;

        public Position(int x, int y)
        {
            _position = new Vector2(x, y);
        }

        public Position(Vector2 position)
        {
            _position = position;
        }
    }
}
