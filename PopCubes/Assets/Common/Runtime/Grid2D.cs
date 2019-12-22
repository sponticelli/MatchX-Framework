using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.MatchX.Common
{
    public class Grid2D<T>
    {
        public int Width { get; }
        public int Height { get;  }
        
        public List<Cell2D<T>> Cells { get; private set; }

        public Grid2D(int width, int height)
        {
            Width = width;
            Height = height;
            
            Cells = new List<Cell2D<T>>(Width*Height);
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Cells.Add(new Cell2D<T>(new Vector2Int(x, y)));
                }
            }
        }

        #region Cell Index
        public int CellIndex(Vector2Int pos)
        {
            return CellIndex(pos.x, pos.y);
        }

        public int CellIndex(int posX, int posY)
        {
            return posY * Width + posX;
        }

        public bool IsValid(int cellIndex)
        {
            return cellIndex >= 0 && cellIndex < Cells.Count;
        }

        public bool IsValid(Vector2Int pos)
        {
            return IsValid(CellIndex(pos));
        }

        public bool IsValid(int posX, int posY)
        {
            return IsValid(CellIndex(posX, posY));
        }
        #endregion
        
        #region Cells

        public Cell2D<T> Cell(int cellIndex)
        {
            return IsValid(cellIndex) ? Cells[cellIndex] : null;
        }

        public Cell2D<T> Cell(Vector2Int pos)
        {
            return Cell(CellIndex(pos));
        }

        public Cell2D<T> Cell(int posX, int posY)
        {
            return Cell(CellIndex(posX, posY));
        }
        #endregion
        
        #region Data
        public T Data(int cellIndex)
        {
            var c = Cell(cellIndex);
            return c != null ? c.Data : default(T);
        }

        public T Data(Vector2Int pos)
        {
            return Data(CellIndex(pos));
        }

        public T Data(int posX, int posY)
        {
            return Data(CellIndex(posX, posY));
        }

        public bool SetData(int cellIndex, T data)
        {
            var c = Cell(cellIndex);
            if (c == null) return false;
            c.Data = data;
            return true;
        }
        public bool SetData(Vector2Int pos, T data)
        {
            return SetData(CellIndex(pos), data);
        }

        public bool SetData(int posX, int posY, T data)
        {
            return SetData(CellIndex(posX, posY), data);
        }
        #endregion
    }
}