using SoloGames.Patterns;

namespace SoloGames.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
            Initialization();
        }

        protected void Initialization()
        {

        }
        
    }
}
