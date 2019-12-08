using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopCubes
{
    [CreateAssetMenu(fileName = "Resolution", menuName = "ScriptableObjects/ResolutionOverride", order = 1)]
    public class ResolutionOverrideScriptableObject : ScriptableObject
    {
        public string resolutionName;
        public int width;
        public int height;
        public float zoomLevel;
        public float canvasScalingMatch;
    }
}
