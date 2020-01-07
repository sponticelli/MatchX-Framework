using System;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using ZigZaggle.Core.Cameras;
using ZigZaggle.Core.FSM;

namespace ZigZaggle.Collapse.Components
{
    public class GridStartState : MBState
    {
        [Header("Camera")] 
        [SerializeField] private CameraTrackTargets cameraTrackTargets;

        private GridController controller;
        private bool initialized;
        
        private void OnEnable()
        {
            controller = (GridController) StateMachine;
            initialized = false;
        }

        private void OnDisable()
        {
            
        }

        private void Update()
        {
            InitGameLogic();
        }

        private void InitGameLogic()
        {
            if (!initialized)
            {
                controller.Logic = new GameLogic(2);
                Clear();
                CreateCells();
                CreateBlocks();

                cameraTrackTargets.SetTargets(controller.Cells.Select(c => c.gameObject).ToArray());
                cameraTrackTargets.UpdateCamera();

                initialized = true;
            }

            StateMachine.Next();
        }

        private void Clear()
        {
            foreach (var cell in controller.Cells)
            {
                Destroy(cell.gameObject);
            }

            foreach (var block in controller.Blocks)
            {
                Destroy(block.gameObject);
            }
            
            controller.Cells.Clear();
            controller.Blocks.Clear();
        }

        private void CreateCells()
        {
            for (var y = 0; y < controller.Logic.Height; y++)
            {
                for (var x = 0; x < controller.Logic.Width; x++)
                {
                    var cellGo = controller.CreateCellGo(x, y);
                    controller.Cells.Add(cellGo);
                }
            }
        }

        private void CreateBlocks()
        {
            for (var y = 0; y < controller.Logic.Height; y++)
            {
                for (var x = 0; x < controller.Logic.Width; x++)
                {
                    var color = controller.Logic.Grid.Data(x, y).color;
                    var blockGo = CreateColoredBlock(x, y, color);
                    controller.Blocks.Add(blockGo);
                }
            }
        }

        private ColoredBlock CreateColoredBlock(int x, int y, BlockColors color)
        {
            var blockGo = Instantiate(GetColoredBlockPrefab(color));
            blockGo.Position = new Vector2Int(x, y);
            blockGo.name = "Block_" + color;
            blockGo.transform.position = controller.GetCellPosition(x, y);
            if (controller.blockContainer)
            {
                blockGo.transform.parent = controller.blockContainer.transform;
            }

            return blockGo;
        }

        private ColoredBlock GetColoredBlockPrefab(BlockColors color)
        {
            var idx = (int) color;
            idx %= controller.coloredBlockPrefabs.Length;
            return controller.coloredBlockPrefabs[idx];
        }
    }
}