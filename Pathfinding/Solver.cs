using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    /// <summary>
    /// Contains methods for creating a Graph
    /// object out of a collection of edges and
    /// finding the lengths of the shortest paths
    /// from a starging node to all other nodes.
    /// </summary>
    public static class Solver
    {
        public static Paths<V> FindShortestPaths<V>(IEnumerable<IEdge<V>> edges, V start)
        {
            Graph<V> graph = ConstructGraph(edges);
            Paths<V> answer = FindShortestPaths(graph, start);
            return answer;
        }

        public static Graph<V> ConstructGraph<V>(IEnumerable<IEdge<V>> edges)
        {
            Graph<V> graph = new() { Structure = new() };

            foreach (IEdge<V> edge in edges)
            {
                if (!graph.Structure.ContainsKey(edge.NodeA))
                    graph.Structure.Add(edge.NodeA, new());
                graph.Structure[edge.NodeA].Add(edge);

                if (!graph.Structure.ContainsKey(edge.NodeB))
                    graph.Structure.Add(edge.NodeB, new());
                graph.Structure[edge.NodeB].Add(edge);
            }

            return graph;
        }

        public static Paths<V> FindShortestPaths<V>(Graph<V> graph, V start)
        {
            List<V> nodes = graph.Structure.Keys.ToList();
            int nodeCount = nodes.Count;

            Dictionary<V, IEdge<V>> parents = new(nodeCount);
            Dictionary<V, double> weights = new(nodeCount);

            BinaryHeap<NodeState<V>> heap = new(nodeCount);

            foreach (V node in nodes)
                weights[node] = double.MaxValue;

            weights[start] = 0;
            heap.Add(new(0, start));
            while (heap.Count != 0)
            {
                NodeState<V> state = heap.Pop();

                if (state.weight != weights[state.node])
                    continue;

                V current = state.node;

                foreach (IEdge<V> edge in graph.Structure[current])
                {
                    V other = edge.Other(current);

                    // The comparison is done this way to prevent overflow,
                    // because the initial weight amounts are the type max value.
                    if (weights[current] >= weights[other] - edge.Weight)
                        continue;

                    weights[other] = weights[current] + edge.Weight;
                    parents[other] = edge;
                    heap.Add(new(weights[other], other));
                }
            }

            return new(start, parents, weights);
        }

        private struct NodeState<V> : IComparable<NodeState<V>>
        {
            public double weight;
            public V node;

            public NodeState(double weight, V node)
            {
                this.weight = weight;
                this.node = node;
            }

            public int CompareTo(NodeState<V> other)
            {
                return other.weight.CompareTo(weight);
            }
        }
    }
}
