using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BigBlue
{
    public class Level
    {
        private string _name;
        public string Name { get { return _name; } }

        public int Width { get { return Layout1.GetLength(0); } }
        public int Height { get { return Layout1.GetLength(1); } }
        //public int Width { get; set; }
        //public int Height { get; set; }

        // The following definitions depend on if you want to parse out the levels into char[] immediately from the file or do it in the WorldCreator
        // Either way, we would need to parse a string into a list of char[] for the layouts. Depends on if you want to do it when initially reading the levels file
        // or when creating the level. I prefer to do it when reading the levels file, because we'll already be able to access the file line by line, and can add
        // char arrays to a list much simpler.
        // List<char[]> could also just be a char[,] (2D char array)
        //public List<char[]> layout1;
        //public List<char[]> layout2;
        //
        //OR
        //
        //public string layout1;
        //public string layout2;

        // Choosing char[,] so I can verify the width/height
        public char[,] Layout1;
        public char[,] Layout2;

        // Note: because we need to be able to generate all Levels from levels-all.bbiy, I'm going to let LevelManager handle file parsing.
        public Level(string name, char[,] layout1, char[,] layout2)
        {
            _name = name;
            Layout1 = layout1;
            Layout2 = layout2;
        }

        //public Level(string name, int width, int height)
        //{
        //    Name = name;
        //    Width = width;
        //    Height = height;
        //}
    }
}
