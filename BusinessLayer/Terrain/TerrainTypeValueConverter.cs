using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Terrain
{
    public static class TerrainTypeValueConverter
    {
        public static double TerrainModifier(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Plains => 1,
                TerrainType.Forest => 1.5,
                TerrainType.Mountain => 2,
                TerrainType.Tunnel => 1.75,
                TerrainType.Desert => 1.6,
                TerrainType.Snow => 1.8,
                _ => 1,
            };
        }

        public static double TerrainModifier(TerrainType primary, TerrainType secondary)
        {
            double primaryModifier = TerrainModifier(primary);
            double secondaryModifier = TerrainModifier(secondary);

            return (primaryModifier + secondaryModifier) / 2;
        }
    }
}
