using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Baba
{
    public class Level
    {
        private string _name;
        public string Name { get { return _name; } }

        public int Width { get { return ObjectLayout.GetLength(1); } }
        public int Height { get { return ObjectLayout.GetLength(0); } }

        public char[,] ObjectLayout;  // top layout
        public char[,] TextLayout;  // bottom layout

        // Note: because we need to be able to generate all Levels from levels-all.bbiy, I'm going to let LevelManager handle file parsing.
        public Level(string name, char[,] layout1, char[,] layout2)
        {
            _name = name;
            ObjectLayout = layout1;
            TextLayout = layout2;
        }
    }
}
