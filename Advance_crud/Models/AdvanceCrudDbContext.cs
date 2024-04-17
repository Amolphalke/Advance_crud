using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Advance_crud.Models
{
    public class AdvanceCrudDbContext : DbContext
    {
        public AdvanceCrudDbContext(DbContextOptions<AdvanceCrudDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure cascade delete for Country-State relationship
            modelBuilder.Entity<State>()
                .HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.country_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure cascade delete for State-City relationship
            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.state_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
