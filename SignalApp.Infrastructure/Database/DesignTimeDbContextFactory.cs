using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace SignalApp.Infrastructure.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SignalDbContext>
    {
        public SignalDbContext CreateDbContext(string[] args)
        {
            string current = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(current))
            {
                var sln = Directory.GetFiles(current, "*.sln")
                         .Concat(Directory.GetFiles(current, "*.slnx"))
                         .ToArray();

                if (sln.Length > 0)
                    break;

                current = Directory.GetParent(current)?.FullName!;
            }

            string dbPath = Path.Combine(current, "signals.db");
            string connectionString = $"Data Source={dbPath}";

            var optionsBuilder = new DbContextOptionsBuilder<SignalDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new SignalDbContext(optionsBuilder.Options);
        }
    }
}
