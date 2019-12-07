namespace PopCubes
{
    public class GameGrid : Grid2D<Cell2D<ITile>, ITile>
    {
        public GameGrid(int width, int height) : base(width, height)
        {
            foreach (var cell in _cells)
            {
                cell.Data = new BlockTile() {type = BlockType.Empty};
            }
        }
    }
}