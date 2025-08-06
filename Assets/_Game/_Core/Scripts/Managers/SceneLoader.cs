using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace SoloGames.Managers
{
    public class SceneLoader : MMSingleton<SceneLoader>
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
