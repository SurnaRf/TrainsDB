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
    public class TrainCarTest
    {
        private static TrainDbContext dbContext => SetUpFixture.dbContext;
        private TrainCarContext trainCarContext = new(dbContext);
        private TrainCar trainCar;
        private Location location;
        private TrainComposition trainComposition;

        [SetUp]
        public async Task SetUp() 
        {
            location = new("Amsterdam", new(52.3, 4.9));

            trainComposition = new("com", TrainType.Passenger, location);

            trainCar = new(TrainCarType.Passenger, 8000.0, location);

            await trainCarContext.CreateAsync(trainCar);
        }

        [TearDown]
        public async Task TaskTearDown() 
        {
            foreach (TrainCar item in dbContext.TrainCars.ToList())
            {
                dbContext.TrainCars.Remove(item);
            }

            await dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task CreateAsync() 
        {
            TrainCar trainCar = new(TrainCarType.Cargo, 8000, location, trainComposition);

            int trainCarsBefore = dbContext.TrainCars.Count();
            
            await trainCarContext.CreateAsync(trainCar);
            
            int trainCarsAfter = dbContext.TrainCars.Count();
            
            Assert.That(trainCarsBefore + 1 == trainCarsAfter, "CreateAsync() does not work!");
        }

        [Test]
        public async Task ReadAsync() 
        {
            TrainCar readTrainCar = await trainCarContext.ReadAsync(trainCar.Id, false, false);

            Assert.That(trainCar, Is.EqualTo(readTrainCar), "ReadAstnc() does not return same object!");
        }

        [Test]
        public async Task ReadWithNavigationPropertiesAsync() 
        {
            TrainCar readTrainCar = await trainCarContext.ReadAsync(trainCar.Id, true, false);

            Assert.That(readTrainCar.Location.Equals(location), "ReadAsync() with nav props does not work!");
        }

        [Test]
        public async Task ReadAllAsync() 
        { 
            List<TrainCar> trainCars = (List<TrainCar>) await trainCarContext.ReadAllAsync();

            Assert.That(trainCars.Count != 0, "ReadAllAsync() does not work!");
        }

        [Test]
        public async Task UpdateAsync() 
        {
            TrainCar changedTrainCar = await trainCarContext.ReadAsync(trainCar.Id);

            changedTrainCar.TrainComposition = trainComposition;
            changedTrainCar.Weight = 12222;

            await trainCarContext.UpdateAsync(changedTrainCar);

            Assert.That(changedTrainCar.TrainComposition, Is.EqualTo(trainComposition), "UpdateAsync() does not work!");
        }

        [Test]
        public async Task DeleteAsync() 
        {
            int trainCarsBefore = dbContext.TrainCars.Count();

            await trainCarContext.DeleteAsync(trainCar.Id);
            int trainCarsAfter = dbContext.TrainCars.Count();

            Assert.That(trainCarsBefore - 1 == trainCarsAfter, "DeleteAsync() does not work!");
        }
    }
}
