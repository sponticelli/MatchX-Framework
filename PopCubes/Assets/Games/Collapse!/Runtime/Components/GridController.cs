using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using ZigZaggle.Collapse.Components;
using ZigZaggle.Core.Audio;
using ZigZaggle.Core.Cameras;
using ZigZaggle.Core.FSM;
using ZigZaggle.Core.UI;
using ZigZaggle.MatchX.Common;
using ZigZaggle.MatchX.Common.Algorithms;
using ZigZaggle.MatchX.Common.Algorithms.Gravity;
using Random = UnityEngine.Random;

namespace ZigZaggle.Collapse
{
    public class GridController : MBStateMachine
    {
        [Header("Prefabs")] public Cell[] cellPrefabs;
        public ColoredBlock[] coloredBlockPrefabs;

        [Header("Elements")] public GameObject cellContainer;
        public GameObject blockContainer;

        public Cell selectedCell;

        [Header("Rules")] public int minMatches = 2;

        [Header("Config")] public float blockMoveDuration = 3f;

        [Header("Audio")] public AudioClip clickBlock;
        public AudioClip errorClickBlock;

        [Header("GUI")] public AnimatedCounter scoreCounter;

        public GameLogic Logic { get; set; }

        public List<Cell> Cells => cells;
        public List<BaseBlock> Blocks => blocks;

        private List<Cell> cells = new List<Cell>();
        private List<BaseBlock> blocks = new List<BaseBlock>();
        public int score;


        private void Start()
        {
            if (cellContainer) cellContainer.transform.position = Vector3.zero;
            if (blockContainer) blockContainer.transform.position = Vector3.zero;

            base.Start();

            score = 0;
            scoreCounter.CurrentValue = score;
        }


        #region Creation

        public Vector3 GetCellPosition(int x, int y)
        {
            var idx = Logic.Grid.CellIndex(x, y);
            return Logic.Grid.IsValid(idx) ? Cells[idx].transform.position : Vector3.negativeInfinity;
        }

        public Cell CreateCellGo(int x, int y)
        {
            var idx = Logic.Grid.CellIndex(x, y);
            var prefabIdx = idx % cellPrefabs.Length;
            var cellGo = Instantiate(cellPrefabs[prefabIdx]);
            cellGo.name = "Cell_" + x + "_" + y;
            var cell = cellGo.GetComponent<Cell>();
            cell.Position = new Vector2Int(x, y);
            var sprite = cellGo.GetComponent<SpriteRenderer>();
            var bounds = sprite.bounds;
            cellGo.transform.position = new Vector3(x * bounds.size.x, -y * bounds.size.y, 0);
            if (cellContainer)
            {
                cellGo.transform.parent = cellContainer.transform;
            }

            return cell;
        }

        #endregion
    }
}