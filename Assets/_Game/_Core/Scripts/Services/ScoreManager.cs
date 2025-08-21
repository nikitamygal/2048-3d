using System;
using SoloGames.Configs;
using SoloGames.Services;

namespace SoloGames.Managers
{
    public class ScoreManager : Service
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
            _score += value;
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

        public int CalculateScores(int value)
        {
            return value / 4;
        }

        private void OnMergeCubes(CubeItemSO newCubeConfig)
        {
            if (newCubeConfig == null) return;

            int newValue = newCubeConfig.GetNumber();
            newValue = CalculateScores(newValue);
            Add(newValue);
        }

        private void OnEnable()
        {
            MergeManager.OnMerge += OnMergeCubes;
        }

        private void OnDisable()
        {
            MergeManager.OnMerge -= OnMergeCubes;
        }
        
    }
}
