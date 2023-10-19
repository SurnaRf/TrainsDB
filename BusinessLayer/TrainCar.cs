using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TrainCar
    {

        [Key]
        public int Id { get; set; }

        public TrainCarType TrainCarType { get; set; }

        //requirements at later date
        public double? Weight { get; set; }

        [Required]
        public Location Location { get; set; }

        [ForeignKey("TrainComposition")]
        public int TrainCompositionTd { get; set; }

        public TrainCar(int trainCompositionTd, Location location, double? weight = 0)
        {
            TrainCompositionTd = trainCompositionTd;
            Location = location;
            Weight = weight;
        }

        public TrainCar() { }
    }
}
