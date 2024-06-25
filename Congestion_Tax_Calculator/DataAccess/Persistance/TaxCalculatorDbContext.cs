using Congestion_Tax_Calculator.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Congestion_Tax_Calculator.DataAccess.Persistance
{
    public class TaxCalculatorDbContext : DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options)
        {
        }

        public DbSet<TollRecord> TollRecords { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property<string>(e => e.PlateNumber).IsRequired();
                entity.Property<string>(nameof(VehicleType)).IsRequired();
            });

            modelBuilder.Entity<TollRecord>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.HasOne(e => e.Vehicle)
                      .WithMany()
                      .HasForeignKey(e => e.Id);
            });

            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>(nameof(VehicleType))
                .HasValue<Car>(nameof(Car))
                .HasValue<Motorbike>(nameof(Motorbike))
                .HasValue<MilitaryVehicle>(nameof(MilitaryVehicle));

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PlateNumber).IsRequired();
                entity.Property(e => e.VehicleType).IsRequired();
            });
        }


    }
}
