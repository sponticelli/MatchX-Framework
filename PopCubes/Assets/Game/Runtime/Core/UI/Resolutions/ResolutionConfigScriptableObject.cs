using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopCubes
{
    [CreateAssetMenu(fileName = "ResolutionConfig", menuName = "ScriptableObjects/ResolutionConfig", order = 1)]
    public class ResolutionConfigScriptableObject : ScriptableObject
    {
        public float defaultZoomLevel;
        public float defaultCanvasScalingMatch;
        public ResolutionOverrideScriptableObject[] resolutions;

        public float GetMatchWidthOrHeight(int screenWidth, int screenHeight)
        {
            foreach (var resolution in resolutions)
            {
                if (resolution.width == screenWidth && resolution.height == screenHeight)
                {
                    return resolution.canvasScalingMatch;
                }
            }

            return defaultCanvasScalingMatch;
        }

        public float GetZoomLevel()
        {
            var zoomLevel = defaultZoomLevel;
            foreach (var resolution in resolutions)
            {
                if (resolution.width == Screen.width && resolution.height == Screen.height)
                {
                    return resolution.zoomLevel;
                }
            }

            return defaultZoomLevel;
        }
    }
}