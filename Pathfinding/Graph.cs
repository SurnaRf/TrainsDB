using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    /// <summary>
    /// Wrapper for an adjacency-esque Dictionary
    /// of Nodes and their corresponding edges.
    /// </summary>
    /// <typeparam name="V">Vertex</typeparam>
    public class Graph<V>
    {
        public Dictionary<V, List<IEdge<V>>> Structure { get; set; }
    }
}
