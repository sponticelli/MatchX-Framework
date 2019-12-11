namespace ZigZaggle.MatchX
{
    public enum BlockType
    {
        Block1,
        Block2,
        Block3,
        Block4,
        Block5,
        RandomBlock,
        Empty,
    }
    
    /// <summary>
    /// The base class for blocks.
    /// </summary>
    public class BlockTile : ITile
    {
        public BlockType type;
    }
}