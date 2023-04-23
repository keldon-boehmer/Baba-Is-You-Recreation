using Microsoft.Xna.Framework;

namespace BigBlue.ECS
{
    internal class Position
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

        public bool Equals(Position other)
        {
            return Coordinates.Equals(other.Coordinates);
        }
    }
}
