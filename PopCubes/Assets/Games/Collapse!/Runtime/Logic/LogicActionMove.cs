using UnityEngine;
using ZigZaggle.MatchX.Common.Algorithms.Gravity;

namespace ZigZaggle.Collapse
{
    public class LogicActionMove : ILogicAction
    {
        public Vector2Int FromPos { get; }
        public Vector2Int ToPos { get; }

        public LogicActionMove(Vector2Int fromPos, Vector2Int toPos)
        {
            FromPos = fromPos;
            ToPos = toPos;
        }

        public LogicActionMove(GravityMovement movement)
        {
            FromPos = movement.fromPos;
            ToPos = movement.toPos;
        }
    }
}