using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Infrastructure.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SignalDbContext>
    {
        public SignalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SignalDbContext>();
            optionsBuilder.UseSqlite("Data Source=signals.db");

            return new SignalDbContext(optionsBuilder.Options);
        }
    }
}
