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
        public TerrainType Primary { get; set; }

        [Required]
        public TerrainType Secondary { get; set; }

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
                    * TerrainTypeValueConverter.TerrainModifier(Primary, Secondary);
            }
        }

        private Connection()
        {

        }

        public Connection(
            TerrainType primary,
            TerrainType secondary,
            Location locationA,
            Location locationB)
        {
            Primary = primary;
            Secondary = secondary;
            LocationA = locationA;
            LocationB = locationB;
        }
    }
}
