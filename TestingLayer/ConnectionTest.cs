using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using BusinessLayer.Terrain;
using DataLayer;
using NUnit.Framework;

namespace TestingLayer
{
    [TestFixture]
    class ConnectionTest
    {
        private static TrainDbContext dbContext => SetUpFixture.dbContext;
        private ConnectionContext connectionContext = new(dbContext);
        private Location locA, locB, locC;
        private Connection conAB, conAC, conBC;

        [SetUp]
        public async Task SetUp()
        {
            locA = new("A", new(0, 0));
            locB = new("B", new(1, 0));
            locC = new("C", new(0, 1));

            conAB = new(TerrainType.Plains, locA, locB);
            conAC = new(TerrainType.Plains, locA, locC);
            conBC = new(TerrainType.Plains, locB, locC);

            await connectionContext.CreateAsync(conAB);
            await connectionContext.CreateAsync(conAC);
            await connectionContext.CreateAsync(conBC);
        }

        [TearDown]
        public async Task TearDown()
        {
            foreach(Connection connection in dbContext.Connections.ToList())
            {
                dbContext.Connections.Remove(connection);
            }

            foreach(Location location in dbContext.Locations.ToList())
            {
                dbContext.Locations.Remove(location);
            }

            await dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task Create()
        {
            Connection newConnection = new(TerrainType.Tunnel, locA, locB);

            int connectionsBefore = dbContext.Connections.Count();
            await connectionContext.CreateAsync(newConnection);

            int connectionsAfter = dbContext.Connections.Count();
            Assert.That(connectionsBefore + 1 == connectionsAfter, "Create() does not work!");
        }

        [Test]
        public async Task Read()
        {
            Connection readConnection = await connectionContext.ReadAsync(conAB.Id, false, false);

            Assert.That(conAB, Is.EqualTo(readConnection), "Read does not return the same object!");
        }

        [Test]
        public async Task ReadWithNavigationalProperties()
        {
            Connection readConnection = await connectionContext.ReadAsync(conAB.Id, true, false);
            
            bool areEqual = readConnection.NodeA.Equals(locA) &&
                readConnection.NodeB.Equals(locB) &&
                readConnection.Equals(conAB);
            
            Assert.That(areEqual, "Read with navigational properties does not work!");
        }

        [Test]
        public async Task ReadWithIsReadOnly()
        {
            Connection readConnection = await connectionContext.ReadAsync(conAB.Id);

            Assert.That(conAB, Is.Not.EqualTo(readConnection), "Read returns a tracked object!");
        }

        [Test]
        public async Task ReadAll()
        {
            HashSet<Connection> connections = new(await connectionContext.ReadAllAsync());

            bool containsAll = connections.Any(c => c.Id == conAB.Id) &&
                connections.Any(c => c.Id == conAC.Id) &&
                connections.Any(c => c.Id == conBC.Id);

            Assert.That(containsAll, "ReadAll() does not work!");
        }

        [Test]
        public async Task Update()
        {
            conAB.NodeA = locC;
            await connectionContext.UpdateAsync(conAB);

            Connection readConnection = await connectionContext.ReadAsync(conAB.Id, true, false);

            Assert.That(readConnection.NodeA, Is.EqualTo(locC), "Update() does not work!");
        }

        [Test]
        public async Task Delete()
        {
            int connectionsBefore = dbContext.Connections.Count();
            await connectionContext.DeleteAsync(conAB.Id);

            int connectionsAfter = dbContext.Connections.Count();
            Assert.That(connectionsBefore - 1 == connectionsAfter, "Delete() does not work!");
        }

        [Test]
        public void DeleteThrow()
        {
            Assert.That(() => connectionContext.DeleteAsync(int.MaxValue),
                Throws.InstanceOf(typeof(InvalidOperationException)),
                "Delete() does not throw when given invalid key!");
        }

    }
}
