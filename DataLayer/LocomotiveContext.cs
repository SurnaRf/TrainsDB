using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class LocomotiveContext : IDb<Locomotive, int>
	{
		private readonly TrainDbContext dbContext;

        public LocomotiveContext(TrainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Locomotive item)
		{
			try
			{
				Location locationFromDb = await dbContext.Locations.FindAsync(item.LocationId);

				if (locationFromDb != null)
				{
					item.Location = locationFromDb;
				}

				if(item.TrainCompositionId != null)
				{
					TrainComposition trainCompositionFromDb = await dbContext.TrainCompositions.FindAsync(item.TrainCompositionId);

					if (trainCompositionFromDb != null)
					{
						item.TrainComposition = trainCompositionFromDb;
					}
				}
                else item.TrainComposition = null;

                dbContext.Locomotives.Add(item);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception) { throw; }
		}

		public async Task<Locomotive> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<Locomotive> query = dbContext.Locomotives;

				if (useNavigationalProperties)
				{
					query = query
						.Include(l => l.Location)
						.Include(l => l.TrainComposition);
				}

				if (isReadOnly)
				{
					query = query.AsNoTrackingWithIdentityResolution();
				}

				return await query.FirstOrDefaultAsync(l => l.Id == key);
            }
            catch (Exception) { throw; }
        }

		public async Task<ICollection<Locomotive>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<Locomotive> query = dbContext.Locomotives;

				if (useNavigationalProperties)
				{
					query = query.Include(l => l.Location).Include(l => l.TrainComposition);
				}

				if (isReadOnly)
				{
					query = query.AsNoTrackingWithIdentityResolution();
				}

				return await query.ToListAsync();
			}
            catch (Exception) { throw; }
        }

		public async Task UpdateAsync(Locomotive item, bool useNavigationalProperties = false)
		{
			try
			{
				Locomotive locomotiveFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

				if (locomotiveFromDb == null)
				{
					await CreateAsync(item);
					return;
				}

				dbContext.Entry(locomotiveFromDb).CurrentValues.SetValues(item);

				if (useNavigationalProperties)
				{
					Location locationFromDb = await dbContext.Locations.FindAsync(item.LocationId);

					if (locationFromDb != null)
					{
						locomotiveFromDb.Location = locationFromDb;
					}
					else
					{
						locomotiveFromDb.Location = item.Location;
					}

					TrainComposition trainCompositionFromDb = await dbContext
						.TrainCompositions.FindAsync(item.TrainCompositionId);

					if (trainCompositionFromDb != null)
					{
						locomotiveFromDb.TrainComposition = trainCompositionFromDb;
					}
					else
					{
						locomotiveFromDb.TrainComposition = item.TrainComposition;
					}
				}

				await dbContext.SaveChangesAsync();
			}
            catch (Exception) { throw; }
        }
		
		public async Task DeleteAsync(int key)
		{
			try
			{
				Locomotive locomotiveFromDb = await ReadAsync(key, false, false)
					?? throw new InvalidOperationException("Locomotive with the given key does not exist!");

                dbContext.Locomotives.Remove(locomotiveFromDb);
				await dbContext.SaveChangesAsync();
			}
            catch (Exception) { throw; }
        }
	}
}
