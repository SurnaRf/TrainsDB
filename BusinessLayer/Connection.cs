using System.ComponentModel;
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
        [DisplayName("Location A")]
        public Location NodeA { get; set; }

        [DisplayName("Location A")]
        [ForeignKey("NodeA")]
        public int NodeAId { get; set; }

        [Required]
        [DisplayName("Location B")]
        public Location NodeB { get; set; }

        [DisplayName("Location B")]
        [ForeignKey("NodeB")]
        public int NodeBId { get; set; }

        #endregion

        public double Weight
        {
            get
            {
                if (NodeA == null || NodeB == null) return 0;
                return NodeA.Coordinates.Distance(NodeB.Coordinates)
                    * TerrainTypeValueConverter.SpecificModifier(TerrainType);
            }
        }

        public Connection()
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
