using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Terrain
{
    public static class TerrainTypeValueConverter
    {
        public static double TotalModifier(TerrainType terrainType)
        {
            double totalValue = 0;
            int count = 0;

            foreach(TerrainType value in Enum.GetValues<TerrainType>())
            {
                if (!terrainType.Contains(value))
                    continue;

                totalValue += SpecificModifier(value);
                count++;
            }

            return totalValue / Math.Max(count-1, 1);
        }

        public static double SpecificModifier(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Plains => 1,
                TerrainType.Forest => 1.5,
                TerrainType.Mountain => 2,
                TerrainType.Tunnel => 1.4,
                TerrainType.Desert => 1.6,
                TerrainType.Snow => 1.8,
                _ => 0,
            };
        }

        public static bool Contains(this TerrainType container, TerrainType value)
        {
            return (container & value) != 0;
        }

        public static void Set(ref TerrainType container, TerrainType value, bool set = true)
        {
            if (set) container |= value;
            else container &= ~value;
        }
    }
}
