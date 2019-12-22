using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common.Algorithms
{
    public class OrthoDiagonalNeighbours<T> : OrthoNeighbours<T>
    {
        public override IEnumerable<Vector2Int> Directions { get; } = new[]
        {
            Vector2Int.up,
            Vector2Int.up + Vector2Int.right, 
            Vector2Int.right,
            Vector2Int.right + Vector2Int.down,
            Vector2Int.down,
            Vector2Int.down + Vector2Int.left,
            Vector2Int.left,
            Vector2Int.left + Vector2Int.up
        };
    }
}