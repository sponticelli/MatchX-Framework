using ZigZaggle.MatchX.Common;

namespace ZigZaggle.Collapse
{
    public class LogicActionRemove : ILogicAction
    {
        public Cell2D<Block> Cell { get; private set; }

        public LogicActionRemove(Cell2D<Block> cell)
        {
            Cell = cell;
        }
    }
}