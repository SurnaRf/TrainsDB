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
                optionsBuilder.UseSqlServer("Server=MSWIN;Database=TrainDb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locomotive>()
                .Property(l => l.LocomotiveType)
                .HasConversion<string>();
            modelBuilder.Entity<TrainCar>()
                .Property(tc => tc.TrainCarType)
                .HasConversion<string>();
            modelBuilder.Entity<TrainComposition>()
                .Property(trcn => trcn.TrainType)
                .HasConversion<string>();
            modelBuilder.Entity<Connection>()
                .Property(c => c.TerrainType)
                .HasConversion<string>();

            modelBuilder.Entity<Location>()
                .Property(c => c.Coordinates)
                .HasConversion(new CoordinatesConvertor());

            modelBuilder.Entity<Locomotive>()
                .HasOne(l => l.TrainComposition)
                .WithMany(trcn => trcn.Locomotives)
                .HasForeignKey(l => l.TrainCompositionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrainCar>()
                .HasOne(tc => tc.TrainComposition)
                .WithMany(trcn => trcn.TrainCars)
                .HasForeignKey(tc => tc.TrainCompositionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.NodeA)
                .WithMany(l => l.ConnectionsA)
                .HasForeignKey(c => c.NodeAId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.NodeB)
                .WithMany(l => l.ConnectionsB)
                .HasForeignKey(c => c.NodeBId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Locomotive> Locomotives { get; set; }

        public DbSet<TrainCar> TrainCars { get; set; }

        public DbSet<TrainComposition> TrainCompositions { get; set; }
    }
}