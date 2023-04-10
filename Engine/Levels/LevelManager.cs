using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBlue
{
    public class LevelManager
    {
        private List<Level> Levels;

        private int currentLevelIndex;

        private static LevelManager instance;

        private LevelManager()
        {
            currentLevelIndex = 0;
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
            return Levels[currentLevelIndex];
        }

        public void changeCurrentLevelIndex(int change)
        {
            currentLevelIndex += change;
        }

        public void addLevel(Level newLevel)
        {
            Levels.Add(newLevel);
        }
    }
}
