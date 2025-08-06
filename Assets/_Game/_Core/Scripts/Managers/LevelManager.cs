using MoreMountains.Tools;


namespace SoloGames.Managers
{
    public class LevelManager : MMSingleton<LevelManager>
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
