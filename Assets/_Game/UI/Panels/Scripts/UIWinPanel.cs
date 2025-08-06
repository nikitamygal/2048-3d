using System;
using MoreMountains.Feedbacks;
using SoloGames.Managers;
using UnityEngine;
using UnityEngine.UI;


namespace SoloGames.UI
{
    public class UIWinPanel : UIPanel
    {
        [SerializeField] private Button _btnContinue;
        [SerializeField] private MMFeedbacks _btnContinueFeedbacks;

        protected SceneLoader _sceneLoader => SceneLoader.Instance;

        protected void OnContinue()
        {
            _btnContinueFeedbacks?.PlayFeedbacks();
            _sceneLoader.LoadScene(Scenes.GamePlay);
        }

        private void OnEnable()
        {
            _btnContinue?.onClick.AddListener(OnContinue);
        }
        
        private void OnDisable()
        {
            _btnContinue?.onClick.RemoveListener(OnContinue);
        }
    }
}
