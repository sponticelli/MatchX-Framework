using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using ZigZaggle.Collapse.Components;
using ZigZaggle.Core.Audio;
using ZigZaggle.Core.Cameras;
using ZigZaggle.Core.UI;
using ZigZaggle.MatchX.Common;
using ZigZaggle.MatchX.Common.Algorithms;
using ZigZaggle.MatchX.Common.Algorithms.Gravity;
using Random = UnityEngine.Random;

namespace ZigZaggle.Collapse
{
    public class GridController : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private CameraTrackTargets cameraTrackTargets;
        [SerializeField] private Camera mainCamera;

        [Header("Prefabs")] [SerializeField] private Cell[] cellPrefabs;
        [SerializeField] private ColoredBlock[] coloredBlockPrefabs;

        [Header("Elements")] [SerializeField] private GameObject cellContainer;
        [SerializeField] private GameObject blockContainer;

        [Header("Rules")] [SerializeField] private int minMatches = 2;

        [Header("Config")] [SerializeField] private float blockMoveDuration = 3f;

        [Header("Audio")] [SerializeField] private AudioClip clickBlock;
        [SerializeField] private AudioClip errorClickBlock;

        [Header("GUI")] [SerializeField] private AnimatedCounter scoreCounter;

        private GameLogic gameLogic;
        private List<Cell> cells = new List<Cell>();
        private List<BaseBlock> blocks = new List<BaseBlock>();
        private int score;

        private void Start()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }

            score = 0;
            scoreCounter.CurrentValue = score;

            if (cellContainer) cellContainer.transform.position = Vector3.zero;
            if (blockContainer) blockContainer.transform.position = Vector3.zero;

            gameLogic = new GameLogic(minMatches);
            CreateCells();
            CreateBlocks();

            cameraTrackTargets.SetTargets(cells.Select(c => c.gameObject).ToArray());
            cameraTrackTargets.UpdateCamera();
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Cell"))
            {
                return;
            }

            var cell = hit.collider.gameObject.GetComponent<Cell>();
            if (!cell)
            {
                return;
            }


            StartCoroutine(ExecuteMove(cell));
        }

        private IEnumerator ExecuteMove(Cell cell)
        {
            var actionGroups = gameLogic.MakeMove(cell.Position);
            if (actionGroups.IsNullOrEmpty())
            {
                SimpleSoundPlayer.Play(errorClickBlock);
                yield break;
            }

            for (var i = 0; i < actionGroups.Count; i++)
            {
                var group = actionGroups[i];
                switch (group.Type)
                {
                    case GroupedLogicActions.GroupType.Remove:
                        SimpleSoundPlayer.Play(clickBlock);
                        var matches = group.Actions;
                        score += matches.Count * 55;
                        scoreCounter.CurrentValue = score;
                        DestroyColoredBlocks(matches);
                        yield return new WaitForSeconds(i == actionGroups.Count - 1 ? 0 : 0.2f);
                        break;
                    case GroupedLogicActions.GroupType.Move:
                        MoveBlocks(group.Actions);
                        yield return new WaitForSeconds(i == actionGroups.Count - 1 ? 0 : blockMoveDuration);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void MoveBlocks(IList<ILogicAction> movements)
        {
            if (movements.IsNullOrEmpty()) return;
            foreach (var m in movements)
            {
                var move = (LogicActionMove) m;
                var block = FindByPosition(move.FromPos);
                if (!block) continue;
                block.Position = move.ToPos;
                block.Move(GetCellPosition(move.ToPos.x, move.ToPos.y), blockMoveDuration);
            }
        }

        private void DestroyColoredBlocks(IList<ILogicAction> matches)
        {
            if (matches.IsNullOrEmpty()) return;
            var blockToDestroy = new List<Block>();
            foreach (var m in matches)
            {
                var match = (LogicActionRemove) m;
                var block = FindByPosition(match.Cell.Position);
                ExplodeBlock(block);
            }
        }

        private static void ExplodeBlock(BaseBlock block)
        {
            if (block)
            {
                block.Position = new Vector2Int(int.MinValue, int.MaxValue);
                block.Explode();
            }
        }

        private BaseBlock FindByPosition(Vector2Int pos)
        {
            foreach (var block in blocks)
            {
                if (block.Position.x == pos.x && block.Position.y == pos.y) return block;
            }

            return null;
        }

        #region Creation

        private void CreateCells()
        {
            for (var y = 0; y < gameLogic.Height; y++)
            {
                for (var x = 0; x < gameLogic.Width; x++)
                {
                    var cellGo = CreateCellGo(x, y);
                    cells.Add(cellGo);
                }
            }
        }

        private void CreateBlocks()
        {
            for (var y = 0; y < gameLogic.Height; y++)
            {
                for (var x = 0; x < gameLogic.Width; x++)
                {
                    var color = gameLogic.Grid.Data(x, y).color;
                    var blockGo = CreateColoredBlock(x, y, color);
                    blocks.Add(blockGo);
                }
            }
        }

        private ColoredBlock CreateColoredBlock(int x, int y, BlockColors color)
        {
            var blockGo = Instantiate(GetColoredBlockPrefab(color));
            blockGo.Position = new Vector2Int(x, y);
            blockGo.name = "Block_" + color;
            blockGo.transform.position = GetCellPosition(x, y);
            if (blockContainer)
            {
                blockGo.transform.parent = blockContainer.transform;
            }

            return blockGo;
        }

        private ColoredBlock GetColoredBlockPrefab(BlockColors color)
        {
            var idx = (int) color;
            idx %= coloredBlockPrefabs.Length;
            return coloredBlockPrefabs[idx];
        }

        private Vector3 GetCellPosition(int x, int y)
        {
            var idx = gameLogic.Grid.CellIndex(x, y);
            return gameLogic.Grid.IsValid(idx) ? cells[idx].transform.position : Vector3.negativeInfinity;
        }

        private Cell CreateCellGo(int x, int y)
        {
            var idx = gameLogic.Grid.CellIndex(x, y);
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