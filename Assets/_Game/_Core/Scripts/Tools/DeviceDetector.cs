using UnityEngine;

namespace SoloGames.Tools
{
    public static class DeviceDetector
    {
        public static bool IsMobile()
        {
            return Application.isMobilePlatform;
        }

        public static bool IsDesktop()
        {
            return Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.OSXPlayer ||
                Application.platform == RuntimePlatform.LinuxPlayer ||
                Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.OSXEditor;
        }
    }
}
