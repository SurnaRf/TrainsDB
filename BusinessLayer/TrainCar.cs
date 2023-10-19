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

        #region Navigation

        [Required]
        public Location Location { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [Required]
        public TrainComposition TrainComposition { get; set; }

        [ForeignKey("TrainComposition")]
        public int TrainCompositionId { get; set; }

        #endregion

        public TrainCar() { }
        
        public TrainCar(
            TrainCarType trainCarType,
            Location location,
            double? weight = 0,
            TrainComposition trainComposition = null)
        {
            TrainCarType = trainCarType; 
            Location = location;
            Weight = weight;
            TrainComposition = trainComposition;
        }
    }
}
