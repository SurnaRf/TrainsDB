using BusinessLayer;
using Pathfinding;

namespace TrainsMVC.Models
{
    public class PathQuery
    {
        public int LocationFromId { get; set; }
        public Location? LocationFrom { get; set; }
        
        public int LocationToId { get; set; }
        public Location? LocationTo { get; set; }

        public Paths<Location>? Paths { get; set; }
        public List<IEdge<Location>>? Path { get; set; }
    }
}
