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
        
        public TrainType TrainType { get; set; }

        #region Navigation

        [Required]
		public Location Location { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        public Locomotive LocomotiveA { get; set; }
        
        [ForeignKey("LocomotiveA")]
        public int LocomotiveAId { get; set; }

		public Locomotive LocomotiveB { get; set; }

		[ForeignKey("LocomotiveB")]
		public int LocomotiveBId { get; set; }

        public List<TrainCar> TrainCars { get; set; }

        #endregion

        public TrainComposition()
        {
            TrainCars = new();
        }

        public TrainComposition(
            TrainType trainType,
            Location location,
            Locomotive locomotiveA = null,
            Locomotive locomotiveB = null)
            : this()
		{
            TrainType = trainType;
            Location = location;
			LocomotiveA = locomotiveA;
            LocomotiveB = locomotiveB;
        }
	}
}
