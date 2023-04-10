using System.Collections.Generic;

namespace BigBlue
{
    public class LevelManager
    {
        private List<Level> levels;

        public int currentLevelIndex;

        private static LevelManager instance;


        private LevelManager()
        {
            currentLevelIndex = 0;
            levels = new List<Level>();
        }

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
        public Level getCurrentLevel()
        {
            return levels[currentLevelIndex];
        }

        public void addLevel(Level newLevel)
        {
            levels.Add(newLevel);
        }

        #region Level Select Menuing
        public int LevelCount { get { return levels.Count; } }
        public void addCurrentLevelIndex()
        {
            if (currentLevelIndex < levels.Count - 1)
            {
                currentLevelIndex++;
            }
        }

        public void subtractCurrentLevelIndex()
        {
            if (currentLevelIndex > 0)
            {
                currentLevelIndex--;
            }
        }

        public bool equalsCurrentLevelIndex(int n)
        {
            return currentLevelIndex == n;
        }

        public string getLevelName(int index)
        {
            return levels[index].Name;
        }

        #endregion

    }
}
