using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZaggle.UnityExtensions;
using Object = System.Object;

namespace PopCubes
{
    public class Grid2D<TCell, TData> : IEnumerable where TCell : Cell2D<TData>
    {
        private List<TCell> _cells;
        public int Height { get; }
        public int Width { get; }


        public Grid2D(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new List<TCell>();
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    _cells.Add((TCell) Cell2D.CreateInstance<TData>(i, j));
                }
            }
        }

        public int CellIndex(int i, int j)
        {
            return j * Width + i;
        }

        public TData GetData(int i, int j)
        {
            return _cells[CellIndex(i, j)].Data;
        }

        public void SetData(int i, int j, TData data)
        {
            _cells[CellIndex(i, j)].Data = data;
        }

        public Cell2DEnum<TCell, TData> GetEnumerator()
        {
            return new Cell2DEnum<TCell, TData>(_cells);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }

    public class Cell2DEnum<TCell, TData> : IEnumerator where TCell : Cell2D<TData>
    {
        private List<TCell> _cells;
        int currentIndex = -1;

        public Cell2DEnum(List<TCell> cells)
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

        public TCell Current
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