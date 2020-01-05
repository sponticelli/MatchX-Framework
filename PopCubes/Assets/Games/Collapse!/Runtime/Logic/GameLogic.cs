using System.Collections.Generic;
using UnityEngine;
using ZigZaggle.MatchX.Common;
using ZigZaggle.MatchX.Common.Algorithms;
using ZigZaggle.MatchX.Common.Algorithms.Gravity;

namespace ZigZaggle.Collapse
{
    public class GameLogic
    {
        private IMatchMaker<Block> matchMaker = new OrthoMatchMaker<Block>();
        private IGravity<Block> verticalGravityMaker = new DownGravity<Block>();
        private IGravity<Block> horizontalGravityMaker = new RightGravity<Block>();
        private int minMatches;

        public GameLogic(int minMatches)
        {
            CreateGrid(minMatches);
        }

        #region Grid Info

        public Grid2D<Block> Grid { get; private set; }

        public int Height => Grid.Height;
        public int Width => Grid.Width;

        #endregion

        #region Logic

        public List<Cell2D<Block>> MakeMove(Vector2Int position)
        {
            var cell2d = Grid.Cell(position);
            if (cell2d.Data.type != BlockTypes.Normal) return null;

            var matches = matchMaker.Find(Grid, cell2d,
                (block, block1) => block.type == block1.type && block.color == block1.color);
            if (matches.IsNullOrEmpty() || matches.Count < minMatches) return null;

            foreach (var m in matches)
            {
                Debug.Log("Match " + m.Position.x + ", " + m.Position.y + " " + m.Data.color);
                Grid.SetData(m.Position, new Block(BlockTypes.Empty));
            }

            DebugGrid("Move");
            return matches;
        }

        public List<List<GravityMovement>> ApplyGravity()
        {
            var result = new List<List<GravityMovement>>();
            var vertical = ApplyVerticalGravity();
            if (!vertical.IsNullOrEmpty()) result.Add(vertical);
            var horizontal = ApplyHorizontalGravity();
            if (!horizontal.IsNullOrEmpty()) result.Add(horizontal);
            return result;
        }
        
        protected List<GravityMovement> ApplyVerticalGravity()
        {
            return verticalGravityMaker.Apply(Grid,
                block => block.type == BlockTypes.Normal,
                block => block.type == BlockTypes.Empty,
                () => new Block(BlockTypes.Empty)
            );
        }

        protected List<GravityMovement> ApplyHorizontalGravity()
        {
            var result = horizontalGravityMaker.Apply(Grid,
                block => block.type == BlockTypes.Normal,
                block => block.type == BlockTypes.Empty,
                () => new Block(BlockTypes.Empty)
            );
            return result;
        }

        #endregion

        private void CreateGrid(int minMatches)
        {
            this.minMatches = minMatches;
            Grid = new Grid2D<Block>(10, 10);
            for (var x = 0; x < Grid.Width; x++)
            {
                for (var y = 0; y < Grid.Height; y++)
                {
                    var color = Random.Range((int) BlockColors.Blue, (int) BlockColors.Purple);
                    Grid.SetData(x, y, new Block(BlockTypes.Normal, (BlockColors) color));
                }
            }

            DebugGrid("Created");
        }

        public void DebugGrid(string info)
        {
            Debug.Log(info);
            var s = GridToString();
            Debug.Log(s);
        }

        public string GridToString()
        {
            var s = "";
            for (var y = 0; y < Grid.Height; y++)
            {
                s += "\r\n";
                for (var x = 0; x < Grid.Width; x++)
                {
                    s += Grid.Data(x, y).type == BlockTypes.Empty
                        ? "-"
                        : Grid.Data(x, y).color.ToString().Substring(0, 1);
                }
            }

            return s;
        }
    }
}