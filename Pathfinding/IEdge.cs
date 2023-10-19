using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    /// <summary>
    /// Represents an edge between
    /// two nodes and its weight.
    /// </summary>
    /// <typeparam name="V">Vertex</typeparam>
    public interface IEdge<V>
    {
        V NodeA { get; }
        V NodeB { get; }
        double Weight { get; }
    }
}
