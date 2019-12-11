using System.Resources;

namespace ZigZaggle.MatchX
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

    public class Cell2D<T>: Node<T>
    {

        public CellIndex Index { get; }
        public int I => Index.I;
        public int J => Index.J;

        public Cell2D(int i, int j)
        {
            Index = new CellIndex(i, j);
            Reset();
        }

        
        
    }
}