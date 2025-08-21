using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;


namespace SoloGames.UI
{
    public class UILosePanel : UIPanel
    {
        [SerializeField] private Button _btnRetry;
        [SerializeField] private MMFeedbacks _btnRetryFeedbacks;

        protected void OnRetry()
        {
            _btnRetryFeedbacks?.PlayFeedbacks();
            _sceneLoader.LoadScene(Scenes.GamePlay);
        }

        private void OnEnable()
        {
            _btnRetry?.onClick.AddListener(OnRetry);
        }
        
        private void OnDisable()
        {
            _btnRetry?.onClick.RemoveListener(OnRetry);
        }
    }
}
