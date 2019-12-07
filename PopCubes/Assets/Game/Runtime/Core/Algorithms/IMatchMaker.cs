using System;
using System.Collections.Generic;

namespace PopCubes
{
    public interface IMatchMaker<TCell, TData> where TCell : Cell2D<TData>
    {
         List<TCell> Find(Grid2D<TCell, TData> grid, TCell startingCell, Func<TData, TData, bool> evaluator);
    }
}