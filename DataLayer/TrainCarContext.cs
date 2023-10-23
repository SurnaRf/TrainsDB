using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TrainCarContext : IDb<TrainCar, int>
    {
        private readonly TrainDbContext dbContext;

        public TrainCarContext() 
        {
            dbContext = new TrainDbContext();    
        }

        public async Task CreateAsync(TrainCar item)
        {
            try
            {
                this.dbContext.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                TrainCar trainCarFromDb = await ReadAsync(key, false, false);

                if (trainCarFromDb != null)
                {
                    dbContext.TrainCars.Remove(trainCarFromDb);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<TrainCar>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<TrainCar> query = dbContext.TrainCars;

                if (useNavigationalProperties)
                {
                    query.Include(x => x.Location).Include(x => x.TrainComposition);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TrainCar> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<TrainCar> query = dbContext.TrainCars;

                if (useNavigationalProperties)
                {
                    query.Include(x => x.Location).Include(x => x.TrainComposition);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(x => x.Id == key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(TrainCar item, bool useNavigationalProperties = false)
        {
            try
            {
                TrainCar trainCarFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                if (trainCarFromDb == null) 
                { 
                    await CreateAsync(item);
                    return;
                }

                dbContext.Entry(trainCarFromDb).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    TrainComposition trainCompositionFromDb = await dbContext.TrainCompositions.FindAsync(item.TrainCompositionId);

                    if (trainCompositionFromDb != null)
                    {
                        item.TrainComposition = trainCompositionFromDb;
                    }
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
