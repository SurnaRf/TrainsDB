using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public static class Extensions
    {
        public static V Other<V>(this IEdge<V> edge, V node)
        {
            if (!node.Equals(edge.NodeA)) return edge.NodeA;
            return edge.NodeB;
        }
    }
}
