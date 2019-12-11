using System.Collections.Generic;
using System.Linq;

namespace ZigZaggle.MatchX
{

    public class Node<T> 
    {
        public Graph<T> Graph { get; internal set; }
        public List<Edge<T>> InboundEdges
        {
            get { return Graph.Edges.Where(e => e.To == this).ToList(); }
        }

        public List<Edge<T>> OutboundEdges
        {
            get { return Graph.Edges.Where(e => e.From == this).ToList(); }
        }
        
        public T Data { get; set; }

        public Node()
        {
            Reset();
        }

        public Node(T data)
        {
            Data = data;
        }

        public void Reset()
        {
            Data = default(T);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}