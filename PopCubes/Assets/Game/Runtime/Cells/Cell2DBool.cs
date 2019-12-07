namespace PopCubes
{
    public class Cell2DBool : Cell2D<bool>
    {
        public Cell2DBool(int i, int j) : base(i, j)
        {
            Reset();
        }


        public override void Reset()
        {
            Data = false;
        }
    }
}