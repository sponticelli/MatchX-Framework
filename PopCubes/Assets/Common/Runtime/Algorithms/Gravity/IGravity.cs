using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common.Algorithms.Gravity
{
    public struct GravityMovement
    {
        public Vector2Int fromPos;
        public Vector2Int toPos;
    }
    
    public interface IGravity<T>
    {
        List<GravityMovement> Apply(Grid2D<T> grid, Func<T, bool> canMove, Func<T, bool> isEmpty, Func<T> emptyData);
    }
}