using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engine
{
    [Serializable]
    public class HighScores
    {
        public int[] scores;

        public HighScores()
        {
            scores = new int[5] {0, 0, 0, 0, 0};
        }

        public void saveNewScore(int score)
        {
            int scoreSpot = -1;
            for (int i = 0; i < scores.Length; i++)
            {
                if (score > scores[i])
                {
                    scoreSpot = i;
                    break;
                }
            }

            if (scoreSpot > -1)
            {
                for (int i = scores.Length - 1; i > scoreSpot; i--)
                {
                    scores[i] = scores[i - 1];
                }
                scores[scoreSpot] = score;
            }
        }

        public void resetScores()
        {
            scores = new int[5] { 0, 0, 0, 0, 0 };
        }
    }
}
