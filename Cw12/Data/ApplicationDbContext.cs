using Microsoft.EntityFrameworkCore;
using Cw12.Data.Models;

namespace Cw12.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTrip> CountryTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Client>()
                .ToTable("Client")
                .HasKey(c => c.IdClient);

            mb.Entity<Trip>()
                .ToTable("Trip")
                .HasKey(t => t.IdTrip);

            mb.Entity<Country>()
                .ToTable("Country")
                .HasKey(c => c.IdCountry);


            mb.Entity<ClientTrip>(entity =>
            {
                entity.ToTable("Client_Trip");
                entity.HasKey(ct => new { ct.IdClient, ct.IdTrip });

                entity
                    .HasOne(ct => ct.Client)
                    .WithMany(c => c.ClientTrips)
                    .HasForeignKey(ct => ct.IdClient)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(ct => ct.Trip)
                    .WithMany(t => t.ClientTrips)
                    .HasForeignKey(ct => ct.IdTrip)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            mb.Entity<CountryTrip>(entity =>
            {
                entity.ToTable("Country_Trip");
                entity.HasKey(ct => new { ct.IdCountry, ct.IdTrip });

                entity
                    .HasOne(ct => ct.Country)
                    .WithMany(c => c.CountryTrips)
                    .HasForeignKey(ct => ct.IdCountry)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasOne(ct => ct.Trip)
                    .WithMany(t => t.CountryTrips)
                    .HasForeignKey(ct => ct.IdTrip)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(mb);
        }



    }
}