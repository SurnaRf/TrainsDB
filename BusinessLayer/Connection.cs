using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLayer.Terrain;
using Pathfinding;

namespace BusinessLayer
{
    public class Connection : IEdge<Location>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TerrainType TerrainType { get; set; }

        #region Navigation

        [Required]
        public Location NodeA { get; set; }

        [ForeignKey("NodeA")]
        public int NodeAId { get; set; }

        [Required]
        public Location NodeB { get; set; }
        
        [ForeignKey("NodeB")]
        public int NodeBId { get; set; }

        #endregion

        public double Weight
        {
            get
            {
                return NodeA.Coordinates.Distance(NodeB.Coordinates)
                    * TerrainTypeValueConverter.TotalModifier(TerrainType);
            }
        }

        private Connection()
        {

        }

        public Connection(
            TerrainType terrainType,
            Location locationA,
            Location locationB)
        {
            TerrainType = terrainType;
            NodeA = locationA;
            NodeB = locationB;
        }
    }
}
