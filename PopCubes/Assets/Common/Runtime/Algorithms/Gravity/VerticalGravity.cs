using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common.Algorithms.Gravity
{
    public class VerticalGravity<T> : IGravity<T>
    {
        public List<GravityMovement> Apply(Grid2D<T> grid, Func<T, bool> moveable, Func<T, bool> isEmpty, Func<T> emptyData)
        {
            var result = new List<GravityMovement>();

            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = grid.Height - 2; y >= 0; y--)
                {
                    var cell = grid.Cell(x, y);
                    if (moveable(cell.Data))
                    {
                        var fromPos = cell.Position;
                        var toPos = cell.Position;
                        Cell2D<T> nextCell = null;
                        //Search lowest cell
                        for (var j = y + 1; j < grid.Height; j++)
                        {
                            nextCell = grid.Cell(x, j);
                            if ((nextCell == null) || (!isEmpty(nextCell.Data))) break;
                            toPos = nextCell.Position;
                        }

                        if (fromPos != toPos)
                        {
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
                }
            }

            return result;
        }
        
    }
}