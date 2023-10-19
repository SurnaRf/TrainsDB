using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class Location
	{

        [Key]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

		public Coordinates Coordinates { get; set; }

		public List<TrainComposition> TrainCompositions { get; set; }

		public List<Locomotive> Locomotives { get; set; }

		public List<TrainCar> TrainCars { get; set; }

        public Location() 
        {
            TrainCompositions = new List<TrainComposition>();
            Locomotives = new List<Locomotive>();
            TrainCars = new List<TrainCar>();
        }

        public Location(string? name, Coordinates coordinates)
        {
            Name = name;
            Coordinates = coordinates;
            TrainCompositions = new List<TrainComposition>();
            Locomotives = new List<Locomotive>();
            TrainCars = new List<TrainCar>();
        }


    }
}
