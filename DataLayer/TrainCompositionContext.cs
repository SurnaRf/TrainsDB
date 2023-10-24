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
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<TrainComposition> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<TrainComposition> query = dbContext.TrainCompositions;

				if (useNavigationalProperties)
				{
					query = query.Include(trnc => trnc.Location)
						.Include(trnc => trnc.LocomotiveA)
						.Include(trnc => trnc.LocomotiveB)
						.Include(trnc => trnc.TrainCars);
				}

				return await query.FirstOrDefaultAsync(trnc => trnc.Id == key);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<ICollection<TrainComposition>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
		{
			try
			{
				IQueryable<TrainComposition> query = dbContext.TrainCompositions;

				if (useNavigationalProperties)
				{
					query = query.Include(trnc => trnc.Location)
						.Include(trnc => trnc.LocomotiveA)
						.Include(trnc => trnc.LocomotiveB)
					.Include(trnc => trnc.TrainCars);
				}

				return await query.ToListAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task UpdateAsync(TrainComposition item, bool useNavigationalProperties = false)
		{
			try
			{
				TrainComposition trainCompositionFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

				if (trainCompositionFromDb == null)
				{
					CreateAsync(item);
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

					Locomotive locomotiveA = await dbContext.Locomotives.FindAsync(item.LocomotiveAId);

					if(locomotiveA != null)
					{
						trainCompositionFromDb.LocomotiveA = locomotiveA;
					}
					else
					{
						trainCompositionFromDb.LocomotiveA = item.LocomotiveA;
					}

					Locomotive locomotiveB = await dbContext.Locomotives.FindAsync(item.LocomotiveBId);

					if (locomotiveB != null)
					{
						trainCompositionFromDb.LocomotiveB = locomotiveB;
					}
					else
					{
						trainCompositionFromDb.LocomotiveB = item.LocomotiveB;
					}

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
			catch (Exception)
			{

				throw;
			}
		}

		public async Task DeleteAsync(int key)
		{
			try
			{
				TrainComposition trainCompositionFromDb = await ReadAsync(key);

				if (trainCompositionFromDb == null)
				{
					throw new ArgumentException("Train composition with the given key does not exist!");
				}

				dbContext.TrainCompositions.Remove(trainCompositionFromDb);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
