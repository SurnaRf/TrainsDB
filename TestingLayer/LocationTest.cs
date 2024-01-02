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
    public class LocationTest
    {
        private static TrainDbContext dbContext => SetUpFixture.dbContext;
        private LocationContext locationContext = new(dbContext);
        private Location location;
        private Coordinates coordinates;

        [SetUp]
        public async Task SetUp()
        {
            coordinates = new(52.3, 4.9);
            location = new("Amsterdam", coordinates);

            await locationContext.CreateAsync(location);
        }


        [TearDown]
        public async Task TearDown()
        {

            foreach (Location item in dbContext.Locations.ToList())
            {
                dbContext.Locations.Remove(item);
            }

            await dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task CreateAsync()
        {
            Location location = new("Burno", new(12.3, 10.4));

            int locationsBefore = dbContext.Locations.Count();

            await locationContext.CreateAsync(location);

            int locationsAfter = dbContext.Locations.Count();

            Assert.That(locationsBefore + 1 == locationsAfter, "CreateAsync() does not work!");
        }

        [Test]
        public async Task ReadAsync()
        {
            Location readLocation = await locationContext.ReadAsync(location.Id, false, false);

            Assert.That(location, Is.EqualTo(readLocation), "ReadAsync() does not return the same object!");
        }

        [Test]
        public async Task ReadWithNavigationalPropertiesAsync()
        {
            Location readLocation = await locationContext.ReadAsync(location.Id, true, false);

            Assert.That(
                readLocation.Coordinates.Equals(coordinates),
                "ReadAsync() with navigational properties does not work!");
        }


        [Test]
        public async Task ReadAllAsync()
        {
            List<Location> locations = (List<Location>)await locationContext.ReadAllAsync();

            Assert.That(locations.Count != 0, "ReadAllAsync() does not work!");
        }

        [Test]

        public async Task UpdateAsync()
        {
            Location changedLocation = await locationContext.ReadAsync(location.Id);

            changedLocation.Name = "Loc2";
            changedLocation.Coordinates = coordinates;

            await locationContext.UpdateAsync(changedLocation);

            Assert.That(changedLocation.Coordinates, Is.EqualTo(coordinates), "UpdateAsync() does not work!");
        }

        [Test]
        public async Task DeleteAsync()
        {
            int locationsBefore = dbContext.Locations.Count();

            await locationContext.DeleteAsync(location.Id);
            int locationsAfter = dbContext.Locations.Count();

            Assert.That(locationsBefore - 1 == locationsAfter, "DeleteAsync() does not work!");
        }
    }
}
