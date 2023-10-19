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
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public User()
        {
            
        }
		public User(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}
	}
}
