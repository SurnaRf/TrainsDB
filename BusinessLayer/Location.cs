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

		public string Name { get; set; }

		public Coordinates Coordinates { get; set; }

        #region Navigation

        public List<Connection> ConnectionsA { get; set; }
        public List<Connection> ConnectionsB { get; set; }

        public List<TrainComposition> TrainCompositions { get; set; }

		public List<Locomotive> Locomotives { get; set; }

		public List<TrainCar> TrainCars { get; set; }

        #endregion
        
        public Location() 
        {
            ConnectionsA = new();
            ConnectionsB = new();
            TrainCompositions = new();
            Locomotives = new();
            TrainCars = new();
        }

        public Location(string name, Coordinates coordinates)
            : this()
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}
