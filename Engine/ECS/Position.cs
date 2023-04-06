using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue.ECS
{
    internal class Position : Component
    {
        private Vector2 _position;
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
