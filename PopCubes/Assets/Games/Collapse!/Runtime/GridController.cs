using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using ZigZaggle.Collapse.Components;
using ZigZaggle.Core.Cameras;
using ZigZaggle.MatchX.Common;
using ZigZaggle.MatchX.Common.Algorithms;
using ZigZaggle.MatchX.Common.Algorithms.Gravity;
using Random = UnityEngine.Random;

namespace ZigZaggle.Collapse
{
    public class GridController : MonoBehaviour
    {
 
        [Header("Camera")]
        [SerializeField] private CameraTrackTargets cameraTrackTargets;
        [SerializeField] private Camera mainCamera;
        
        [Header("Prefabs")]
        [SerializeField] private Cell[] cellPrefabs;
        [SerializeField] private ColoredBlock[] coloredBlockPrefabs;
        
        [Header("Elements")] 
        [SerializeField] private GameObject cellContainer;
        [SerializeField] private GameObject blockContainer;

        [Header("Rules")] 
        [SerializeField] private int minMatches = 2;
        
        private Grid2D<Block> grid;
        private List<Cell> cells = new List<Cell>();
        private List<BaseBlock> blocks = new List<BaseBlock>();
        private IMatchMaker<Block> matchMaker = new OrthoMatchMaker<Block>();
        private IGravity<Block> gravityMaker = new VerticalGravity<Block>();

        private void Start()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }

            if (cellContainer) cellContainer.transform.position = Vector3.zero;
            if (blockContainer) blockContainer.transform.position = Vector3.zero;
            
            CreateGrid(); DebugGrid("CREATED");
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
            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Cell")) return;
            var cell = hit.collider.gameObject.GetComponent<Cell>();
            if (!cell) return;
            var cell2d = grid.Cell(cell.Position);
            if (cell2d.Data.type != BlockTypes.Normal) return;
            var matches = matchMaker.Find(grid, cell2d, 
                (block, block1) => block.type == block1.type && block.color == block1.color);

            if (matches.IsNullOrEmpty() || matches.Count < minMatches) return;
            
            DestroyColoredBlocks(matches);
            foreach (var m in matches)
            {
                grid.SetData(m.Position, new Block(BlockTypes.Empty));
            }
            DebugGrid("DESTROY");

            var movements = gravityMaker.Apply(grid,
                block => block.type == BlockTypes.Normal,
                block => block.type == BlockTypes.Empty,
                () => new Block(BlockTypes.Empty)
            );

            MoveBlocks(movements);
            DebugGrid("GRAVITY");
        }

        private void MoveBlocks(List<GravityMovement> movements)
        {
            foreach (var move in movements)
            {
                Debug.Log("From " + move.fromPos + " To " + move.toPos);
                var block = FindByPosition(move.fromPos);
                if (!block) continue;
                block.Position = move.toPos;
                block.Move(GetCellPosition(move.toPos.x, move.toPos.y), 1f);
            }
        }

        private void DestroyColoredBlocks(List<Cell2D<Block>> matches)
        {
            var blockToDestroy = new List<Block>();
            foreach (var m in matches)
            {
                var block = FindByPosition(m.Position);
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
        private void CreateGrid()
        {
            grid = new Grid2D<Block>(4, 4);
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var color = Random.Range((int) BlockColors.Blue, (int)BlockColors.Purple);
                    grid.SetData(x, y, new Block(BlockTypes.Normal,(BlockColors) color));
                }
            }
        }
        
        private void CreateCells()
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var cellGo = CreateCellGo(x, y);
                    cells.Add(cellGo);
                }
            }
        }

        private void CreateBlocks()
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var color = grid.Data(x, y).color;
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
            var idx = grid.CellIndex(x, y);
            return grid.IsValid(idx) ? cells[idx].transform.position : Vector3.negativeInfinity;
        }
        
        private Cell CreateCellGo(int x, int y)
        {
            var idx = grid.CellIndex(x, y);
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

        private void DebugGrid(string info)
        {
            Debug.Log(info);
            var s = "";
            for (var y = 0; y < grid.Height; y++)
            {
                s += "\r\n";
                for (var x = 0; x < grid.Width; x++)
                {
                    s += grid.Data(x, y).type == BlockTypes.Empty
                        ? "-"
                        : grid.Data(x, y).color.ToString().Substring(0, 1);
                }
               
            }
            Debug.Log(s);
        }
    }
    
    
}