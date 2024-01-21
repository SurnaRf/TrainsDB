using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class User
	{
		[Key]
		public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public User() { }
		
        public User(string firstName, string lastName)
		{
			Id = Guid.NewGuid().ToString();
			FirstName = firstName;
			LastName = lastName;
		}
	}
}
