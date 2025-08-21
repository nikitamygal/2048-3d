using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SoloGames.UI
{
    public class UIWinPanel : UIPanel
    {
        [Header("Bindings")]
        [SerializeField] private TextMeshProUGUI _txtNewCube;
        [SerializeField] private Button _btnContinue;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _btnContinueFeedbacks;

        private void Start()
        {
            SetTxtNewCube((int)_saveSystem.Data.MaxWinCubeValue);
        }

        private void SetTxtNewCube(int number)
        {
            if (_txtNewCube == null) return;
            _txtNewCube.text = number.ToString();
        }

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
