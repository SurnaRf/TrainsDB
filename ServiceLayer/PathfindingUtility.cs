using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using DataLayer;
using Pathfinding;

namespace ServiceLayer
{
    public class PathfindingUtility
    {
        public static async Task<Paths<Location>> GetPaths(
            Location start,
            IDb<Connection, int> connectionContext)
        {
            var edges = await connectionContext.ReadAllAsync(true);
            Paths<Location> result = Solver.FindShortestPaths(edges, start);
            return result;
        }
    }
}
