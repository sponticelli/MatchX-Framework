using System.Resources;

namespace PopCubes
{
    public class CellIndex
    {
        public int I { get; }
        public int J { get; }
        public CellIndex(int i, int j)
        {
            I = i;
            J = j;
        }
    }
    public static class Cell2D
    {
        public static Cell2D<T> CreateInstance<T>(int i, int j)
        {
            return new Cell2D<T>(i, j);
        }
    }

    public class Cell2D<T>
    {
        public T Data { get; set; }
        public CellIndex Index { get; }
        public int I => Index.I;
        public int J => Index.J;

        public Cell2D(int i, int j)
        {
            Index = new CellIndex(i, j);
            Reset();
        }

        public void Reset()
        {
            Data = default(T);
        }
        
    }
}