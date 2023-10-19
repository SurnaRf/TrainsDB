using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
                return NodeA.Coordinates.Distance(NodeB.Coordinates);
            }
        }

        private Connection()
        {

        }

        public Connection(Location locationA, Location locationB)
        {
            LocationA = locationA;
            LocationB = locationB;
        }
    }
}
