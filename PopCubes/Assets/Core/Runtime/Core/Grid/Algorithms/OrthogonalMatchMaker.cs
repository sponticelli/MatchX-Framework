using System;
using System.Collections.Generic;
using System.Linq;

namespace ZigZaggle.MatchX
{
    public class OrthogonalMatchMaker<T> : IMatchMaker<T>
    {


        private readonly List<CellIndex> _orthoNeighbours = new List<CellIndex>()
        {
            new CellIndex(0,-1) , //TOP
            new CellIndex(-1,0), // LEFT
            new CellIndex(1, 0), //RIGHT
            new CellIndex(0, 1) //BOTTOM
        };

        public List<Cell2D<T>> Find(OrthoGrid2D<T> orthoGrid, Cell2D<T> startingCell, Func<T, T, bool> evaluator)
        {
            var result = new List<Cell2D<T>>();
            var stack = new Stack<Cell2D<T>>();
            stack.Push(startingCell);
            var visitedCells = new OrthoGrid2D<bool>(orthoGrid.Width, orthoGrid.Height);
            visitedCells.SetData(startingCell.I, startingCell.J, true);
            while (stack.Count > 0)
            {
                var cell = stack.Pop();
                if (!evaluator(cell.Data, startingCell.Data)) continue;
                result.Add(cell);
                CheckNeighbours(orthoGrid, cell, visitedCells, stack);
            }

            return result;
        }

        private void CheckNeighbours(OrthoGrid2D<T> orthoGrid, Cell2D<T> cell, OrthoGrid2D<bool> visitedCells,
            Stack<Cell2D<T>> stack)
        {
            foreach (var o in _orthoNeighbours)
            {
                var i = cell.I + o.I;
                var j = cell.J + o.J;
                if (i < 0 || i >= orthoGrid.Width || j < 0 || j >= orthoGrid.Height) continue;
                var neighbour = orthoGrid.GetCell(i, j);
                if (visitedCells.GetData(i, j)) continue;
                stack.Push(neighbour);
                visitedCells.SetData(i, j, true);
            }
        }
    }
}