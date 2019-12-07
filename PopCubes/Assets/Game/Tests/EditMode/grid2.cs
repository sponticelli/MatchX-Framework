using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PopCubes;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class grid2
    {
        // A Test behaves as an ordinary method
        [Test]
        public void create_square_grid()
        {
            int width = 10;
            int height = 10;
            var grid = new Grid2D<Cell2D<int>, int>(width,  height);
            foreach (var cell in grid)
            {
                cell.Data = width * cell.J + cell.I;
            }

            var cellData = grid.GetData(width / 2, height / 2);
            Assert.AreEqual(grid.CellIndex(width/2, height/2), cellData);
        }

        
    }
}
