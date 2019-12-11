using ZigZaggle.MatchX;
using UnityEngine;
using UnityEngine.UI;

namespace GameVanilla.Game.UI
{
    public class AdjustCanvasToResolution : MonoBehaviour
    {
        public ResolutionConfigScriptableObject resolutionConfig;
        private CanvasScaler canvasScaler;
        private int resolutionX;
        private int resolutionY;
        
        private void Awake()
        {
            canvasScaler = GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = resolutionConfig.GetMatchWidthOrHeight(Screen.width, Screen.height);
        }
        
#if UNITY_EDITOR
        private void Update ()
        {
            if (resolutionX == Screen.width && resolutionY == Screen.height) return;
            canvasScaler.matchWidthOrHeight = resolutionConfig.GetMatchWidthOrHeight(Screen.width, Screen.height);
            resolutionX = Screen.width;
            resolutionY = Screen.height;
        }
#endif //UNITY_EDITOR
    }
}

