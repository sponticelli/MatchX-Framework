using System.Collections.Generic;

namespace ZigZaggle.MatchX
{
    public class Graph<T>
    {
        public List<Edge<T>> Edges { get; private set; }

        public List<Node<T>> Nodes { get; private set; }

        public Graph()
        {
            Edges = new List<Edge<T>>();
            Nodes = new List<Node<T>>();
        }
        
        public Graph(List<Edge<T>> edges, List<Node<T>> nodes)
        {
            Edges = edges;
            Nodes = nodes;
            foreach (var node in nodes)
            {
                node.Graph = this;
            }
        }

        public void AddEdge(Edge<T> edge)
        {
            Edges.Add(edge);
        }

        public void AddEdge(Node<T> from, Node<T> to)
        {
            Edges.Add(new Edge<T>(from, to));
        }

        public void AddNode(Node<T> node)
        {
            Nodes.Add(node);
            node.Graph = this;
        }

        public void RemoveEdge(Edge<T> edge)
        {
            Edges.Remove(edge);
        }

        public void RemoveNode(Node<T> node)
        {
            Edges.RemoveAll(e => e.From == node || e.To == node);
            Nodes.Remove(node);
        }
    }
}