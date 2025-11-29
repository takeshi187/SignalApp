using Microsoft.EntityFrameworkCore;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Database.Configurations;

namespace SignalApp.Infrastructure.Database
{
    public class SignalDbContext : DbContext
    {
        public SignalDbContext(DbContextOptions<SignalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SignalConfiguration());
            modelBuilder.ApplyConfiguration(new SignalPointConfiguration());
        }

        public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalPoint> SignalPoints { get; set; }
    }
}
