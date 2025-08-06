using System;
using MoreMountains.Tools;

namespace SoloGames.Managers
{
    public class ScoreManager : MMSingleton<ScoreManager>
    {
        public static event Action<int> OnScoreUpdated;

        protected int _score;

        private void Start()
        {
            Reset();
        }

        public int GetCount()
        {
            return _score;
        }

        public void Add(int value)
        {
            _score += CalculateScores(value);
            OnScoreUpdated?.Invoke(_score);
        }

        public void Remove(int amount)
        {
            _score -= amount;
            OnScoreUpdated?.Invoke(_score);
        }

        public void Reset()
        {
            _score = 0;
            OnScoreUpdated?.Invoke(_score);
        }

        private int CalculateScores(int value)
        {
            return value / 2;
        }
        
    }
}
