using System;
using System.Collections.Generic;

namespace ZigZaggle.MatchX.Common.Algorithms
{
    public abstract class AbstractMatchMaker<T> : IMatchMaker<T>
    {
        public List<Cell2D<T>> Find(Grid2D<T> grid, Cell2D<T> startingCell, Func<T, T, bool> evaluator)
        {
            var result = new List<Cell2D<T>>();
            var stack = new Stack<Cell2D<T>>();
            stack.Push(startingCell);
            var visitedCells = new Grid2D<bool>(grid.Width, grid.Height);
            visitedCells.SetData(startingCell.Position.x, startingCell.Position.y, true);
            while (stack.Count > 0)
            {
                var cell = stack.Pop();
                if (!evaluator(cell.Data, startingCell.Data)) continue;
                result.Add(cell);
                CheckNeighbours(grid, cell, visitedCells, stack, evaluator);
            }

            return result;
        }
        protected void CheckNeighbours(Grid2D<T> grid, Cell2D<T> cell, Grid2D<bool> visitedCells,
            Stack<Cell2D<T>> stack,  Func<T, T, bool> evaluator)
        {
            var neighbours = GetNeighbours(cell, grid);            
            
            foreach (var neighbour in neighbours)
            {
                if (visitedCells.Data(neighbour.Position)) continue;
                if (!evaluator(cell.Data, neighbour.Data)) continue;
                stack.Push(neighbour);
                visitedCells.SetData(neighbour.Position, true);
            }
        }

        protected abstract IEnumerable<Cell2D<T>> GetNeighbours(Cell2D<T> cell, Grid2D<T> grid);
    }
}