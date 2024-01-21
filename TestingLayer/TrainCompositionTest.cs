using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
	[TestFixture]
	public class TrainCompositionTest
	{
		private static TrainDbContext dbContext => SetUpFixture.dbContext;
		private TrainCompositionContext trainCompositionContext = new(dbContext);
		private TrainComposition composition;
		private Location location;
		private Locomotive loc1, loc2;
		private TrainCar tc1, tc2;

		[SetUp]
		public async Task SetUp()
		{
			location = new("Amsterdam", new(52.3, 4.9));

			composition = new("com", TrainType.Passenger, location);

			loc1 = new("Loc1", 15000, LocomotiveType.Diesel, location, composition);
			loc2 = new("Loc2", 12000, LocomotiveType.Electric, location, composition);

			tc1 = new(TrainCarType.Passenger, 1500, location, composition);
			tc2 = new(TrainCarType.Passenger, 1200, location, composition);

			composition.TrainCars.Add(tc1);
			composition.TrainCars.Add(tc2);
			composition.Locomotives.Add(loc1);
			composition.Locomotives.Add(loc2);

			await trainCompositionContext.CreateAsync(composition);
		}

		[TearDown]
		public async Task TearDown()
		{
			foreach (TrainComposition item in dbContext.TrainCompositions.ToList())
			{
				dbContext.TrainCompositions.Remove(item);
			}

			await dbContext.SaveChangesAsync();
		}

		[Test]
		public async Task CreateAsync()
		{
			TrainComposition trainComposition = new("com", TrainType.Passenger,location);

			int compositionsBefore = dbContext.TrainCompositions.Count();

			await trainCompositionContext.CreateAsync(trainComposition);

			int compositionsAfter = dbContext.TrainCompositions.Count();

			Assert.That(compositionsBefore + 1 == compositionsAfter, "CreateAsync() does not work!");
		}

		[Test]
		public async Task ReadAsync()
		{
			TrainComposition readComposition = await trainCompositionContext.ReadAsync(composition.Id, false, false);

			Assert.That(composition, Is.EqualTo(readComposition), "ReadAsync() does not return the same object!");
		}

		[Test]
		public async Task ReadWithNavigationalPropertiesAsync()
		{
			TrainComposition readComposition = await trainCompositionContext.ReadAsync(composition.Id, true, false);

			Assert.That(
				readComposition.Location.Equals(location) && readComposition.TrainCars.Contains(tc1)
				&& readComposition.TrainCars.Contains(tc2) && readComposition.Locomotives.Contains(loc1)
				&& readComposition.Locomotives.Contains(loc2),
				"ReadAsync() with navigational properties does not work!");
		}

		[Test]
		public async Task ReadAllAsync()
		{
			List<TrainComposition> compositions = (List<TrainComposition>)await trainCompositionContext.ReadAllAsync();

			Assert.That(compositions.Count != 0, "ReadAllAsync() does not work!");
		}

		[Test]
		public async Task UpdateAsync()
		{
			TrainComposition changedComposition = await trainCompositionContext.ReadAsync(composition.Id);

			changedComposition.Location = location;

			await trainCompositionContext.UpdateAsync(changedComposition);

			Assert.That(changedComposition.Location, Is.EqualTo(location), "UpdateAsync() does not work!");
		}

		[Test]
		public async Task DeleteAsync()
		{
			int compositionsBefore = dbContext.TrainCompositions.Count();

			await trainCompositionContext.DeleteAsync(composition.Id);
			int compositionsAfter = dbContext.TrainCompositions.Count();

			Assert.That(compositionsBefore - 1 == compositionsAfter, "DeleteAsync() does not work!");
		}
	}
}
