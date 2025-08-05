
using SoloGames.Managers;
using TMPro;
using UnityEngine;

namespace SoloGames.UI
{
    public class UIScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCounter;

        public void SetAmount(float amount)
        {
            if (_textCounter == null) return;
            _textCounter.text = amount.ToString();
        }

        private void OnScoreUpdated(int totalAmount)
        {
            SetAmount(totalAmount);
        }

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += OnScoreUpdated;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= OnScoreUpdated;
        }
    }

}