using UnityEngine;

namespace ZigZaggle.MatchX.Common
{
    public class Cell2D<T>
    {
        public Cell2D(Vector2Int pos)
        {
            Position = pos;
            Reset();
        }

        public Cell2D(Vector2Int pos, T data)
        {
            Position = pos;
            Data = data;
        }
        
        public void Reset()
        {
            Data = default(T);
        }

        public T Data { get; set; }
        public Vector2Int Position { get; }
    }
}