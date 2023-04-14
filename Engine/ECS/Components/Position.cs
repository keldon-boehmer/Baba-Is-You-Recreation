using Microsoft.Xna.Framework;

namespace BigBlue
{
    public class Position
    {
        public Vector2 Coordinates;

        public Position(int x, int y)
        {
            Coordinates = new Vector2(x, y);
        }

        public Position(Vector2 position)
        {
            Coordinates = position;
        }

        public Position Clone()
        {
            return new Position(Coordinates);
        }
    }
}
