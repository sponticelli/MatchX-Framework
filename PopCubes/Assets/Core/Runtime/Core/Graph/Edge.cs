namespace ZigZaggle.MatchX
{
    public class Edge<T>
    {        
        public Node<T> From { get; private set; }

        public Node<T> To { get; private set; }

        public Edge(Node<T> from, Node<T> to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From} -> {To}";
        }
    }
}