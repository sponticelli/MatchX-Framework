using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZaggle.UnityExtensions;
using Object = System.Object;

namespace ZigZaggle.MatchX
{
    public class OrthoGrid2D<T> : Graph<T>, IEnumerable
    {
        public int Height { get; }
        public int Width { get; }


        public OrthoGrid2D(int width, int height) : base()
        {
            Width = width;
            Height = height;
            
            CreateNodes();
            CreateEdges();
        }

        private void CreateEdges()
        {
            foreach (var fromNode in Nodes)
            {
                var cell = (Cell2D<T>) fromNode;
                for (var j = cell.J-1; j <= cell.J+1; j++)
                {
                    if (j < 0 || j>=Height) continue;
                    for (var i = cell.I-1; i <= cell.I+1; i++)
                    {
                        if (i<0 || i>=Width || i == cell.I && j == cell.J) continue;
                        var toCell = GetCell(i, j);
                        AddEdge(fromNode, toCell);
                    }
                }
            }
        }

        private void CreateNodes()
        {
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    var cell = new Cell2D<T>(i, j);
                    Nodes.Add(cell);
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
            var node = Nodes[CellIndex(i, j)];
            return node.Data;
        }

        public Cell2D<T> GetCell(int i, int j)
        {
            return (Cell2D<T>)Nodes[CellIndex(i, j)];
        }

        public void SetData(int i, int j, T data)
        {
            Nodes[CellIndex(i, j)].Data = data;
        }

        public NodeEnum<T> GetEnumerator()
        {
            return new NodeEnum<T>(Nodes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }

    public class NodeEnum<T> : IEnumerator
    {
        private readonly List<Node<T>> nodes;
        int currentIndex = -1;

        public NodeEnum(List<Node<T>> nodes)
        {
            this.nodes = nodes;
        }

        public bool MoveNext()
        {
            currentIndex++;
            return (currentIndex < nodes.Count);
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        object IEnumerator.Current => Current;

        public Node<T> Current
        {
            get
            {
                try
                {
                    return nodes[currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}