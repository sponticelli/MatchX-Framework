using System;
using System.Collections.Generic;
using System.Linq;

namespace PopCubes
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

        public List<Cell2D<T>> Find(Grid2D<T> grid, Cell2D<T> startingCell, Func<T, T, bool> evaluator)
        {
            var result = new List<Cell2D<T>>();
            var stack = new Stack<Cell2D<T>>();
            stack.Push(startingCell);
            var visitedCells = new Grid2D<bool>(grid.Width, grid.Height);
            visitedCells.SetData(startingCell.I, startingCell.J, true);
            while (stack.Count > 0)
            {
                var cell = stack.Pop();
                if (!evaluator(cell.Data, startingCell.Data)) continue;
                result.Add(cell);
                CheckNeighbours(grid, cell, visitedCells, stack);
            }

            return result;
        }

        private void CheckNeighbours(Grid2D<T> grid, Cell2D<T> cell, Grid2D<bool> visitedCells,
            Stack<Cell2D<T>> stack)
        {
            foreach (var o in _orthoNeighbours)
            {
                var i = cell.I + o.I;
                var j = cell.J + o.J;
                if (i < 0 || i >= grid.Width || j < 0 || j >= grid.Height) continue;
                var neighbour = grid.GetCell(i, j);
                if (visitedCells.GetData(i, j)) continue;
                stack.Push(neighbour);
                visitedCells.SetData(i, j, true);
            }
        }
    }
}