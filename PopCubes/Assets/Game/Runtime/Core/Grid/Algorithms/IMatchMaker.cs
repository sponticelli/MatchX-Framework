using System;
using System.Collections.Generic;

namespace PopCubes
{
    public interface IMatchMaker<T> 
    {
         List<Cell2D<T>> Find(Grid2D<T> grid, Cell2D<T> startingCell, Func<T, T, bool> evaluator);
    }
}