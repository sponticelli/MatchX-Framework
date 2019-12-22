using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common.Algorithms
{
    public interface INeighbours<T>
    { 
        List<Cell2D<T>> Find(Cell2D<T> cell, Grid2D<T> grid);
    }
}