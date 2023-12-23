using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using DataLayer;

namespace TestingLayer
{
    [SetUpFixture]
    public static class SetUpFixture
    {
        public static TrainDbContext dbContext;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            DbContextOptionsBuilder builder = new();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new(builder.Options);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            dbContext.Dispose();
        }
    }
}
