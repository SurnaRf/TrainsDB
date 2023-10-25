using Microsoft.EntityFrameworkCore;
using BusinessLayer;

namespace DataLayer
{
    public class TrainDbContext : DbContext
    {
        public TrainDbContext() : base()
        {

        }

        public TrainDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning Change connection string
                optionsBuilder.UseSqlServer("Server=;Database=TrainDb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locomotive>().Property(l => l.LocomotiveType).HasConversion<string>();
            modelBuilder.Entity<TrainCar>().Property(l => l.TrainCarType).HasConversion<string>();
            modelBuilder.Entity<TrainComposition>().Property(l => l.TrainType).HasConversion<string>();
            modelBuilder.Entity<Connection>().Property(c => c.TerrainType).HasConversion<string>();
            
            modelBuilder.Entity<Location>().Property(c => c.Coordinates).HasConversion(new CoordinatesConvertor());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Locomotive> Locomotives { get; set; }

        public DbSet<TrainCar> TrainCars { get; set; }

        public DbSet<TrainComposition> TrainCompositions { get; set; }
    }
}