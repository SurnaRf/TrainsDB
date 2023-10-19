using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class TrainComposition
	{
        [Key]
        public int Id { get; set; }

        public Locomotive LocomotiveA { get; set; }

        [ForeignKey("LocomotiveA")]
        public int LocomotiveAId { get; set; }

		public Locomotive LocomotiveB { get; set; }

		[ForeignKey("LocomotiveB")]
		public int LocomotiveBId { get; set; }

        [Required]
		public Location Location { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        //Add List<TrainCar>

        public TrainType TrainType { get; set; }

        public TrainComposition()
        {
            
        }

		public TrainComposition(Locomotive locomotiveA, Locomotive locomotiveB, Location location)
		{
			LocomotiveA = locomotiveA;
			LocomotiveB = locomotiveB;
			Location = location;
		}
	}
}
