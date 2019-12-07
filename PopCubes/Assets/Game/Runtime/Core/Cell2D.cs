using System.Resources;

namespace PopCubes
{
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
        public int I { get; }
        public int J { get; }

        public Cell2D(int i, int j)
        {
            I = i;
            J = j;
        }
        
    }
}