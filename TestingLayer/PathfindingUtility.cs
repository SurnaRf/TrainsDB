using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using BusinessLayer.Terrain;

namespace TestingLayer
{
    static class PathfindingUtility
    {
        public static void GenerateGraphObjects(out List<Location> locations, out List<Connection> connections)
        {
            locations = new();
            connections = new();

            Location locA = new("A", new(2, 1)); locations.Add(locA);
            Location locB = new("B", new(7, 1)); locations.Add(locB);
            Location locC = new("C", new(6, 2)); locations.Add(locC);
            Location locD = new("D", new(1, 4)); locations.Add(locD);
            Location locE = new("E", new(8, 4)); locations.Add(locE);
            Location locF = new("F", new(4, 5)); locations.Add(locF);
            Location locG = new("G", new(7, 5)); locations.Add(locG);
            Location locH = new("H", new(9, 6)); locations.Add(locH);
            Location locI = new("I", new(2, 7)); locations.Add(locI);
            Location locJ = new("J", new(6, 8)); locations.Add(locJ);

            Connection conAB = new(TerrainType.Mountain, locA, locB); connections.Add(conAB);
            Connection conAC = new(TerrainType.Plains, locA, locC); connections.Add(conAC);
            Connection conAD = new(TerrainType.Plains, locA, locD); connections.Add(conAD);
            Connection conBC = new(TerrainType.Plains, locB, locC); connections.Add(conBC);
            Connection conCE = new(TerrainType.Plains, locC, locE); connections.Add(conCE);
            Connection conCF = new(TerrainType.Plains, locC, locF); connections.Add(conCF);
            Connection conDF = new(TerrainType.Plains, locD, locF); connections.Add(conDF);
            Connection conDI = new(TerrainType.Plains, locD, locI); connections.Add(conDI);
            Connection conEG = new(TerrainType.Plains, locE, locG); connections.Add(conEG);
            Connection conEH = new(TerrainType.Mountain, locE, locH); connections.Add(conEH);
            Connection conFJ = new(TerrainType.Plains, locF, locJ); connections.Add(conFJ);
            Connection conGH = new(TerrainType.Plains, locG, locH); connections.Add(conGH);
            Connection conGJ = new(TerrainType.Plains, locG, locJ); connections.Add(conGJ);
            Connection conIJ = new(TerrainType.Plains, locI, locJ); connections.Add(conIJ);
        }
    }
}
