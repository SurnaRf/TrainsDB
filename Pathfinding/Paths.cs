using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    /// <summary>
    /// Wrapper for the result of the search
    /// algorithm performed by the Solver class.
    /// </summary>
    /// <typeparam name="V">Vertex</typeparam>
    public class Paths<V>
    {
        public readonly V Start;
        public readonly Dictionary<V, IEdge<V>> Parents;
        public readonly Dictionary<V, double> Distances;

        public Paths(
            V start,
            Dictionary<V, IEdge<V>> parents,
            Dictionary<V, double> distances)
        {
            this.Start = start;
            this.Parents = parents;
            this.Distances = distances;
        }

        public List<V> ReconstructPath(V destination)
        {
            Stack<V> path = new();

            path.Push(destination);

            while (!destination.Equals(Start))
            {
                destination = Parents[destination].Other(destination);
                path.Push(destination);
            }

            return path.ToList();
        }

        public List<IEdge<V>> ReconstructPathEdges(V destination)
        {
            Stack<IEdge<V>> path = new();

            while (!destination.Equals(Start))
            {
                path.Push(Parents[destination]);
                destination = Parents[destination].Other(destination);
            }

            return path.ToList();
        }
    }
}
