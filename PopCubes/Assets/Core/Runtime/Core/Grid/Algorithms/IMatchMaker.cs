using System;
using System.Collections.Generic;

namespace ZigZaggle.MatchX
{
    public interface IMatchMaker<T> 
    {
         List<Cell2D<T>> Find(OrthoGrid2D<T> orthoGrid, Cell2D<T> startingCell, Func<T, T, bool> evaluator);
    }
}