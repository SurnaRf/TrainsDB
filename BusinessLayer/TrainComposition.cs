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

        public List<Locomotive> Locomotives { get; set; }

        public List<TrainCar> TrainCars { get; set; }

        #endregion

        public TrainComposition()
        {
            TrainCars = new();
            Locomotives = new();
        }

        public TrainComposition(
            TrainType trainType,
            Location location)
            : this()
		{
            TrainType = trainType;
            Location = location;
        }

        public double TotalWeight()
        {
            double total = 0;
            foreach (TrainCar trainCar in TrainCars)
            {
                total += trainCar.Weight;
            }
            return total;
        }
	}
}
