using System;
using System.Collections.Generic;

namespace ZigZaggle.MatchX.Common.Algorithms
{
    public class OrthoMatchMaker<T> : AbstractMatchMaker<T>
    {
        private readonly OrthoNeighbours<T> orthoNeighboursFinder = new OrthoNeighbours<T>();
        protected override IEnumerable<Cell2D<T>> GetNeighbours(Cell2D<T> cell, Grid2D<T> grid)
        {
            return orthoNeighboursFinder.Find(cell, grid);
        }
    }
}