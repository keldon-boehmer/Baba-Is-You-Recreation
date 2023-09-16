using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Baba
{
    public class LevelManager
    {
        private List<Level> _levels = new List<Level>();

        private int _currentLevelIndex = 0;

        private static LevelManager instance;
        private LevelManager() { }

        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelManager();
                }
                return instance;
            }
        }
        public Level GetCurrentLevel()
        {
            return _levels[_currentLevelIndex];
        }

        public void ResetCurrentLevelIndex()
        {
            _currentLevelIndex = 0;
        }

        #region Level Adding
        public void CreateAllLevels(string allLevelsFile = "../../../Content/Levels/levels-all.bbiy")
        {
            string[] lines = File.ReadAllLines(allLevelsFile);
            int i_line = 0;
            while (i_line < lines.Length)
            {
                // Reading the first two lines
                string name = lines[i_line++].Replace("-", " "); ;
                string grid_wh = lines[i_line++];
                string[] wh = grid_wh.Split(' ');
                int width = int.Parse(wh[0]);
                int height = int.Parse(wh[2]);

                // Reading the two layouts
                string[] linesToConvert1 = new string[height];
                Array.Copy(lines, i_line, linesToConvert1, 0, height);
                i_line += height;
                char[,] layout1 = GetChars(width, height, linesToConvert1);
                string[] linesToConvert2 = new string[height];
                Array.Copy(lines, i_line, linesToConvert2, 0, height);
                i_line += height;
                char[,] layout2 = GetChars(width, height, linesToConvert2);

                _levels.Add(new Level(name, layout1, layout2));
            }
        }

        public void CreateLevel(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            // Reading the first two lines
            string name = lines[0].Replace("-", " "); ;
            string grid_wh = lines[1];
            string[] wh = grid_wh.Split(' ');
            int width = int.Parse(wh[0]);
            int height = int.Parse(wh[2]);

            // Reading the two layouts
            string[] linesToConvert1 = new string[height];
            Array.Copy(lines, 2, linesToConvert1, 0, height);
            char[,] layout1 = GetChars(width, height, linesToConvert1);
            string[] linesToConvert2 = new string[height];
            Array.Copy(lines, height + 2, linesToConvert2, 0, height);
            char[,] layout2 = GetChars(width, height, linesToConvert2);

            _levels.Add(new Level(name, layout1, layout2));
        }


        private char[,] GetChars(int width, int height, string[] lines)
        {
            char[,] chars = new char[width, height];
            for (int i = 0; i < height; i++)
            {
                char[] charArray = lines[i].ToCharArray();
                for (int j = 0; j < width; j++)
                {
                    chars[i, j] = charArray[j];
                }
            }
            return chars;
        }

        #endregion


        #region Level Select Menuing
        public int LevelCount { get { return _levels.Count; } }
        public void AddCurrentLevelIndex()
        {
            if (_currentLevelIndex < _levels.Count - 1)
            {
                _currentLevelIndex++;
            }
        }

        public void SubtractCurrentLevelIndex()
        {
            if (_currentLevelIndex > 0)
            {
                _currentLevelIndex--;
            }
        }

        public bool EqualsCurrentLevelIndex(int n)
        {
            return _currentLevelIndex == n;
        }

        public string GetLevelName(int index)
        {
            return _levels[index].Name;
        }

        #endregion

    }
}
