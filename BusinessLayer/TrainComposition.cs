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

        public List<TrainCar> TrainCars { get; set; }

        public TrainType TrainType { get; set; }

        public TrainComposition()
        {
            TrainCars = new List<TrainCar>();
        }

		public TrainComposition( Location location, TrainType trainType, Locomotive locomotiveA = null, Locomotive locomotiveB = null)
		{
            Location = location;
            TrainType = trainType;
			LocomotiveA = locomotiveA;
			LocomotiveB = locomotiveB;			
            TrainCars = new List<TrainCar>();
        }
	}
}
