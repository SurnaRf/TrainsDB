using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class Locomotive
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(15, ErrorMessage = "Name cannot be longer than 15 symbols!")]
        public string Nickname { get; set; }

        [DisplayName("Carrying Capacity")]
        public double CarryingCapacity { get; set; }

        [DisplayName("Locomotive Type")]
        public LocomotiveType LocomotiveType { get; set; }

        #region Navigation

        public Location Location { get; set; }

        [ForeignKey("Location")]
        [DisplayName("Location")]
        public int LocationId { get; set; }

        public TrainComposition? TrainComposition { get; set; }

        [DisplayName("Train Composition")]
        [ForeignKey("TrainComposition")]
        public int? TrainCompositionId { get; set; }

        #endregion

        public Locomotive() { }

		public Locomotive(
            string nickname,
            double carryingCapacity,
            LocomotiveType locomotiveType,
            Location location,
            TrainComposition trainComposition = null)
		{
            Nickname = nickname;
            CarryingCapacity = carryingCapacity;
            LocomotiveType = locomotiveType; 
			Location = location;
            TrainComposition = trainComposition;
		}
    }
}