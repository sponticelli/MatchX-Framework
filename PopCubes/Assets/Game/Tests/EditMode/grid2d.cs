using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PopCubes;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class grid2d
    {
        // A Test behaves as an ordinary method
        [Test]
        public void create_square_grid()
        {
            var width = 10;
            var height = 10;
            var grid = new Grid2D<Cell2D<int>, int>(width, height);
            foreach (var cell in grid)
            {
                cell.Data = width * cell.J + cell.I;
            }

            var cellData = grid.GetData(width / 2, height / 2);
            Assert.AreEqual(grid.CellIndex(width / 2, height / 2), cellData);
        }

        [Test]
        public void orthogonal_matches()
        {
            var width = 4;
            var height = 4;
            var grid = new Grid2D<Cell2D<int>, int>(width, height);
            grid.SetData(0, 0, 1);
            grid.SetData(1, 0, 1);
            grid.SetData(0, 1, 1);
            grid.SetData(1, 1, 1);

            var matchMaker = new OrthogonalMatchMaker<Cell2D<int>, int>();
            var result = matchMaker.Find(grid, grid.GetCell(0, 0), (int val1, int val2) => val1 == val2);
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void create_game_grid()
        {
            var width = 2;
            var height = 2;
            var grid = new GameGrid(width, height);
            grid.SetData(0, 0, new BlockTile() { type = BlockType.Block1});
            grid.SetData(1, 0, new BlockTile() { type = BlockType.Block2});
            grid.SetData(0, 1, new BlockTile() { type = BlockType.Block3});
            grid.SetData(1, 1, new BlockTile() { type = BlockType.Block4});

            var cellData = grid.GetData(0, 0);
            Assert.AreEqual(typeof(BlockTile), cellData.GetType());
            Assert.AreEqual(BlockType.Block1, ((BlockTile)cellData).type);
        }
    }
}