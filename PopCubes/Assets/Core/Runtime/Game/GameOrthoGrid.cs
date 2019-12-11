namespace ZigZaggle.MatchX
{
    public class GameOrthoGrid : OrthoGrid2D<ITile>
    {
        public GameOrthoGrid(int width, int height) : base(width, height)
        {
            foreach (var cell in Nodes)
            {
                cell.Data = new BlockTile() {type = BlockType.Empty};
            }
        }
    }
}