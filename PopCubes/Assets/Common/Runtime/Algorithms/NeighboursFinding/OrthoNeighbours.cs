using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZigZaggle.MatchX.Common;

namespace ZigZaggle.MatchX.Common.Algorithms
{
    public class OrthoNeighbours<T> : INeighbours<T>
    {
        public virtual IEnumerable<Vector2Int> Directions { get; } = new[]
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
        };

        public List<Cell2D<T>> Find(Cell2D<T> cell, Grid2D<T> grid)
        {
            return Find(cell.Position, grid);
        }
        
        public List<Cell2D<T>> Find(Vector2Int pos, Grid2D<T> grid)
        {
            var cellIndex = grid.CellIndex(pos);
            var list = new List<Cell2D<T>>();
            foreach (var dir in Directions)
            {
                var newPos = pos + dir;
                if (grid.IsValid(newPos))
                {
                    list.Add(grid.Cell(newPos));
                }
            }
            return list;
        }
        
        public List<Cell2D<T>> Find(int posX, int posY, Grid2D<T> grid)
        {
            return Find(new Vector2Int(posX, posY), grid);
        }
    }
}