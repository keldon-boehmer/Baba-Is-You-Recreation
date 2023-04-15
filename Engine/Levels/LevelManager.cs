using System.Collections.Generic;

namespace BigBlue
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

        public void AddLevel(Level newLevel)
        {
            _levels.Add(newLevel);
        }

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
