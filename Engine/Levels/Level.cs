namespace BigBlue
{
    public class Level
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

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

        public Level(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

    }
}
