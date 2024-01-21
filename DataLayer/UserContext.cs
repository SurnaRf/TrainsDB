using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BusinessLayer;

namespace DataLayer
{
    public class UserContext : IDb<User, int>
	{
		private readonly TrainDbContext dbContext;

		public UserContext(TrainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

        public async Task CreateAsync(User item)
        {
            try
            {

            }
            catch (Exception) { throw; }
        }

        public async Task<ICollection<User>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                return null;
            }
            catch (Exception) { throw; }
        }

        public async Task<User> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                return null;
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(User item, bool useNavigationalProperties = false)
        {
            try
            {

            }
            catch (Exception) { throw; }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {

            }
            catch (Exception) { throw; }
        }
    }
}
