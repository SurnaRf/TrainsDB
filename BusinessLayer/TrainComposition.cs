using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        public string Name { get; set; }

        [DisplayName("Train Type")]
        public TrainType TrainType { get; set; }

        #region Navigation

        [Required]
		public Location Location { get; set; }

        [ForeignKey("LocationId")]
        [DisplayName("Location")]
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
            string name,
            TrainType trainType,
            Location location)
            : this()
		{
            Name = name;
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

        public const int NoneId = 0;
        public const string NoneName = "<None>";
        public static readonly TrainComposition None = new() { Id = NoneId, Name = NoneName };
	}
}
