using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZaggle.Core.Audio;
using ZigZaggle.Core.FSM;

namespace ZigZaggle.Collapse.Components
{
    public class GridExecuteMoveState : MBState
    {
        private GridController controller;

        private void OnEnable()
        {
            controller = (GridController) StateMachine;
            if (!controller.selectedCell)
            {
                ToInputState();
            }
        }

        private void Update()
        {
            if (!controller.selectedCell) return;
            StartCoroutine(ExecuteMove(controller.selectedCell.Position));
            controller.selectedCell = null;
        }

        private void OnDisable()
        {
            
        }

        private IEnumerator ExecuteMove(Vector2Int cellPosition)
        {
            var actionGroups = controller.Logic.MakeMove(cellPosition);
            if (actionGroups.IsNullOrEmpty())
            {
                SimpleSoundPlayer.Play(controller.errorClickBlock);
                ToInputState();
                yield break;
            }

            for (var i = 0; i < actionGroups.Count; i++)
            {
                var group = actionGroups[i];
                switch (group.Type)
                {
                    case GroupedLogicActions.GroupType.Remove:
                        SimpleSoundPlayer.Play(controller.clickBlock);
                        var matches = group.Actions;
                        controller.score += matches.Count * 55;
                        controller.scoreCounter.CurrentValue = controller.score;
                        DestroyColoredBlocks(matches);
                        yield return new WaitForSeconds(i == actionGroups.Count - 1 ? 0 : 0.2f);
                        break;
                    case GroupedLogicActions.GroupType.Move:
                        MoveBlocks(group.Actions);
                        yield return new WaitForSeconds(i == actionGroups.Count - 1 ? 0 : controller.blockMoveDuration);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            ToInputState();
        }

        private void ToInputState()
        {
            StateMachine.ChangeState("InputState");
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
                block.Move(controller.GetCellPosition(move.ToPos.x, move.ToPos.y), controller.blockMoveDuration);
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
                controller.Blocks.Remove(block);
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
            foreach (var block in controller.Blocks)
            {
                if (block.Position.x == pos.x && block.Position.y == pos.y) return block;
            }

            return null;
        }
    }
}