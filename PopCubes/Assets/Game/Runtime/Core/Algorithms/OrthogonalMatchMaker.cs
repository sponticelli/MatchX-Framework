using System;
using System.Collections.Generic;
using System.Linq;

namespace PopCubes
{
    public class OrthogonalMatchMaker<TCell, TData> : IMatchMaker<TCell, TData> where TCell : Cell2D<TData>
    {
        private struct Coord
        {
            public int I, J;
        }

        private readonly List<Coord> _orthoNeighbours = new List<Coord>()
        {
            new Coord() {I = 0, J = -1}, //TOP
            new Coord() {I = -1, J = 0}, // LEFT
            new Coord() {I = 1, J = 0}, //RIGHT
            new Coord() {I = 0, J = 1} //BOTTOM
        };

        public List<TCell> Find(Grid2D<TCell, TData> grid, TCell startingCell, Func<TData, TData, bool> evaluator)
        {
            var result = new List<TCell>();
            var stack = new Stack<TCell>();
            stack.Push(startingCell);
            var visitedCells = new Grid2D<Cell2D<bool>, bool>(grid.Width, grid.Height);
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

        private void CheckNeighbours(Grid2D<TCell, TData> grid, TCell cell, Grid2D<Cell2D<bool>, bool> visitedCells,
            Stack<TCell> stack)
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