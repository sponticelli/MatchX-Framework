using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common.Algorithms.Gravity
{
    public class RightGravity<T> : IGravity<T>
    {
        public List<GravityMovement> Apply(Grid2D<T> grid, Func<T, bool> canMove, Func<T, bool> isEmpty, Func<T> emptyData)
        {
            var result = new List<GravityMovement>();
            for (var x = grid.Width - 2; x >= 0; x--)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var cell = grid.Cell(x, y);
                    if (!canMove(cell.Data)) continue;
                    var fromPos = cell.Position;
                    var toPos = cell.Position;
                    Cell2D<T> nextCell = null;
                    //Search rightest cell
                    for (var j = x + 1; j < grid.Width; j++)
                    {
                        nextCell = grid.Cell(j, y);
                        if ((nextCell == null) || (!isEmpty(nextCell.Data))) break;
                        toPos = nextCell.Position;
                    }

                    if (fromPos == toPos) continue;
                    //Copy data in empty cell and empty the previous one
                    grid.SetData(toPos, JsonUtility.FromJson<T>(JsonUtility.ToJson(cell.Data)));
                    grid.SetData(fromPos, emptyData());
                    result.Add(new GravityMovement()
                    {
                        fromPos = fromPos,
                        toPos = toPos
                    });
                }
            }

            return result;
        }
    }
}