using System;
using SoloGames.Patterns;

namespace SoloGames.Managers
{
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public static event Action<int> OnScoreUpdated;

        protected int _score;

        public int GetCount()
        {
            return _score;
        }

        public void Add(int amount)
        {
            _score += amount;
            OnScoreUpdated?.Invoke(_score);
        }

        public void Remove(int amount)
        {
            _score -= amount;
            OnScoreUpdated?.Invoke(_score);
        }
        
    }
}
