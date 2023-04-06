using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue
{
    public class Level
    {
        string Name { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        public Level(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

    }
}
