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
	public class TrainCompositionContext : IDb<TrainComposition, int>
	{
		private readonly TrainDbContext dbContext;

        public TrainCompositionContext(TrainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(TrainComposition item)
		{
			try
			{
				Location locationFromDb = await dbContext.Locations.FindAsync(item.LocationId);

				if (locationFromDb != null)
				{
					item.Location = locationFromDb;
				}

				dbContext.TrainCompositions.Add(item);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception) { throw; }
		}

		public async Task<TrainComposition> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<TrainComposition> query = dbContext.TrainCompositions;

				if (useNavigationalProperties)
				{
					query = query.Include(trnc => trnc.Location)
						.Include(trnc => trnc.Locomotives)
						.Include(trnc => trnc.TrainCars);
				}

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(trnc => trnc.Id == key);
			}
            catch (Exception) { throw; }
        }

		public async Task<ICollection<TrainComposition>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<TrainComposition> query = dbContext.TrainCompositions;

				if (useNavigationalProperties)
				{
					query = query.Include(trnc => trnc.Location)
						.Include(trnc => trnc.Locomotives)
						.Include(trnc => trnc.TrainCars);
				}

                if (isReadOnly)
                {
					query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
			}
            catch (Exception) { throw; }
        }

		public async Task UpdateAsync(TrainComposition item, bool useNavigationalProperties = false)
		{
			try
			{
				TrainComposition trainCompositionFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

				if (trainCompositionFromDb == null)
				{
					await CreateAsync(item);
					return;
				}

				dbContext.Entry(trainCompositionFromDb).CurrentValues.SetValues(item);

				if (useNavigationalProperties)
				{
					Location locationFromDb = await dbContext.Locations.FindAsync(item.LocationId);

					if (locationFromDb != null)
					{
						trainCompositionFromDb.Location = locationFromDb;
					}
					else
					{
						trainCompositionFromDb.Location = item.Location;
					}

					List<Locomotive> locomotives = new();

					foreach(Locomotive locomotive in item.Locomotives)
					{
						Locomotive locomotiveFromDb = await dbContext.Locomotives.FindAsync(locomotive.Id);

						if(locomotiveFromDb != null)
						{
							locomotives.Add(locomotiveFromDb);
						}
						else
						{
							locomotives.Add(locomotive);
						}
					}

					trainCompositionFromDb.Locomotives = locomotives;

					List<TrainCar> trainCars = new();

					foreach (TrainCar trainCar in item.TrainCars)
					{
						TrainCar trainCarFromDb = await dbContext.TrainCars.FindAsync(trainCar.Id);

						if (trainCarFromDb != null)
						{
							trainCars.Add(trainCarFromDb);
						}
						else
						{
							trainCars.Add(trainCar);
						}
					}

					trainCompositionFromDb.TrainCars = trainCars;
				}

				await dbContext.SaveChangesAsync();
			}
			catch (Exception) { throw; }
		}

		public async Task DeleteAsync(int key)
		{
			try
			{
				TrainComposition trainCompositionFromDb = await ReadAsync(key, false, false);

				if (trainCompositionFromDb == null)
				{
					throw new InvalidOperationException("Train composition with the given key does not exist!");
				}

				dbContext.TrainCompositions.Remove(trainCompositionFromDb);
				await dbContext.SaveChangesAsync();
			}
            catch (Exception) { throw; }
        }
	}
}
