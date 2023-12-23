using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LocationContext : IDb<Location, int>
    {
        private readonly TrainDbContext dbContext;

        public LocationContext(TrainDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Location item)
        {
            try
            {
                dbContext.Locations.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<ICollection<Location>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Location> query = dbContext.Locations;

                if (useNavigationalProperties)
                {
                    query.Include(x => x.TrainCompositions)
                        .Include(x => x.TrainCars)
                        .Include(x => x.Locomotives);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<Location> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Location> query = dbContext.Locations;
                
                if (useNavigationalProperties)
                {
                    query.Include(x => x.TrainCompositions)
                        .Include(x => x.TrainCars)
                        .Include(x => x.Locomotives);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(x=>x.Id == key);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(Location item, bool useNavigationalProperties = false)
        {
            try
            {
                Location locationFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                if (locationFromDb == null)
                {
                    await CreateAsync(item);
                    return;
                }

                dbContext.Entry(locationFromDb).CurrentValues.SetValues(item);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
        
        public async Task DeleteAsync(int key)
        {
            try
            {
                Location locationFromDb = await ReadAsync(key);
                if (locationFromDb != null)
                {
                    dbContext.Locations.Remove(locationFromDb);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception) { throw; }
        }
    }
}
