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

        [NotMapped]
        public Location NodeA => LocationA;

        [NotMapped]
        public Location NodeB => LocationB;

        [Required]
        public TerrainType TerrainType { get; set; }

        #region Navigation

        [Required]
        public Location LocationA { get; set; }

        [ForeignKey("LocationA")]
        public int LocationAId { get; set; }

        [Required]
        public Location LocationB { get; set; }
        
        [ForeignKey("LocationB")]
        public int LocationBId { get; set; }

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
            LocationA = locationA;
            LocationB = locationB;
        }
    }
}
