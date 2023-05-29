using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakedSisso_2048
{
    class Score
    {
        private int score;
        private int bestScore;
        public Score(int bestScore)
        {
            this.score = 0;
            this.bestScore = bestScore;
        }
        public int GetScore()
        {
            return this.score;
        }
        public int GetBestScore()
        {
            return this.bestScore;
        }
        private void FindBestScore()
        {
            if (this.score > this.bestScore)
            {
                UpdateBestScore();
            }
        }
        public void ResetScore()
        {
            FindBestScore();
            this.score = 0;
        }
        public void UpdateScore(int value)
        {
            this.score = value;
        }

        private void UpdateBestScore()
        {
            this.bestScore = this.score;
        }
    }
}
