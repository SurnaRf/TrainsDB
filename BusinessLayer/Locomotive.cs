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

        public TrainComposition TrainComposition { get; set; }

        [ForeignKey("TrainComposition")]
        public int TrainCompositionId { get; set; }

        [Required]
        public Location Location { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public LocomotiveType LocomotiveType { get; set; }

        public Locomotive()
        {
            
        }

		public Locomotive(string nickname, Location location, LocomotiveType locomotiveType, TrainComposition trainComposition = null)
		{
			Nickname = nickname;			
			Location = location;
            LocomotiveType = locomotiveType; 
            TrainComposition = trainComposition;
		}
	}
}