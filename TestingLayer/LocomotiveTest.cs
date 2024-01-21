using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

using BusinessLayer;
using DataLayer;

namespace TestingLayer
{
	[TestFixture]
	public class LocomotiveTest
	{
		private static TrainDbContext dbContext => SetUpFixture.dbContext;
		private LocomotiveContext locomotiveContext = new(dbContext);
		private Locomotive locomotive;
		private Location location;
		private TrainComposition trainComposition;

		[SetUp]
		public async Task SetUp()
		{
			location = new("Amsterdam", new(52.3, 4.9));

			trainComposition = new("com", TrainType.Passenger, location);

			locomotive = new("Loc1", 12000, LocomotiveType.Electric, location);

			await locomotiveContext.CreateAsync(locomotive);
		}


		[TearDown]
		public async Task TearDown()
		{

			foreach (Locomotive item in dbContext.Locomotives.ToList())
			{
				dbContext.Locomotives.Remove(item);
			}

			await dbContext.SaveChangesAsync();
		}

		[Test]
		public async Task CreateAsync()
		{
			Locomotive locomotive = new("Loc2", 15000, LocomotiveType.Diesel, location, trainComposition);

			int locomotivesBefore = dbContext.Locomotives.Count();

			await locomotiveContext.CreateAsync(locomotive);

			int locomotivesAfter = dbContext.Locomotives.Count();

			Assert.That(locomotivesBefore + 1 == locomotivesAfter, "CreateAsync() does not work!");
		}

		[Test]
		public async Task ReadAsync()
		{
			Locomotive readLocomotive = await locomotiveContext.ReadAsync(locomotive.Id, false, false);

			Assert.That(locomotive, Is.EqualTo(readLocomotive), "ReadAsync() does not return the same object!");
		}

		[Test]
		public async Task ReadWithNavigationalPropertiesAsync()
		{
			Locomotive readLocomotive = await locomotiveContext.ReadAsync(locomotive.Id, true, false);

			Assert.That(
				readLocomotive.Location.Equals(location),
				"ReadAsync() with navigational properties does not work!");
		}


		[Test]
		public async Task ReadAllAsync()
		{
			List<Locomotive> locomotives =  (List<Locomotive>) await locomotiveContext.ReadAllAsync();

			Assert.That(locomotives.Count != 0, "ReadAllAsync() does not work!");
		}

		[Test]

		public async Task UpdateAsync()
		{
			Locomotive changedLocomotive = await locomotiveContext.ReadAsync(locomotive.Id);

			changedLocomotive.Nickname = "Loc2";
			changedLocomotive.TrainComposition = trainComposition;

			await locomotiveContext.UpdateAsync(changedLocomotive);

			Assert.That(changedLocomotive.TrainComposition, Is.EqualTo(trainComposition), "UpdateAsync() does not work!");
		}

		[Test]
		public async Task DeleteAsync()
		{
			int locomotivesBefore = dbContext.Locomotives.Count();

			await locomotiveContext.DeleteAsync(locomotive.Id);
			int locomotivesAfter = dbContext.Locomotives.Count();

			Assert.That(locomotivesBefore - 1 == locomotivesAfter, "DeleteAsync() does not work!");
		}
	}
}
