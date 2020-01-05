namespace ZigZaggle.Collapse
{
    public enum BlockTypes
    {
        Void, //a "hole" in the grid
        Empty, 
        Normal, //a colored cell
    }

    public enum BlockColors
    {
        None,
        Blue,
        Red,
        Yellow,
        Green,
        Purple
    }
    
    public class Block
    {
        public BlockTypes type = BlockTypes.Empty;
        public BlockColors color = BlockColors.None;

        public Block(BlockTypes type, BlockColors color = BlockColors.None)
        {
            this.type = type;
            this.color = type != BlockTypes.Normal ? BlockColors.None : color;
        }

        public override string ToString()
        {
            return "[Type: " + type + " Color: " + color +"]";
        }
    }
}