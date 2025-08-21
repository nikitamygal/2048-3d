using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace SoloGames.Services
{
    public class SceneService : Service
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
