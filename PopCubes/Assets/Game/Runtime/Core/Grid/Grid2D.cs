using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZaggle.UnityExtensions;
using Object = System.Object;

namespace PopCubes
{
    public class Grid2D<T> : IEnumerable
    {
        protected List<Cell2D<T>> _cells;
        public int Height { get; }
        public int Width { get; }


        public Grid2D(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new List<Cell2D<T>>();
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    _cells.Add(new Cell2D<T>(i, j));
                }
            }
        }

        public int CellIndex(Cell2D<T> cell)
        {
            return CellIndex(cell.I, cell.J);
        }

        public int CellIndex(int i, int j)
        {
            return j * Width + i;
        }

        public T GetData(int i, int j)
        {
            return _cells[CellIndex(i, j)].Data;
        }

        public Cell2D<T> GetCell(int i, int j)
        {
            return _cells[CellIndex(i, j)];
        }

        public void SetData(int i, int j, T data)
        {
            _cells[CellIndex(i, j)].Data = data;
        }

        public Cell2DEnum<T> GetEnumerator()
        {
            return new Cell2DEnum<T>(_cells);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }

    public class Cell2DEnum<T> : IEnumerator
    {
        private List<Cell2D<T>> _cells;
        int currentIndex = -1;

        public Cell2DEnum(List<Cell2D<T>> cells)
        {
            _cells = cells;
        }

        public bool MoveNext()
        {
            currentIndex++;
            return (currentIndex < _cells.Count);
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        object IEnumerator.Current => Current;

        public Cell2D<T> Current
        {
            get
            {
                try
                {
                    return _cells[currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}