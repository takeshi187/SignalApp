using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Infrastructure.Database.Configurations
{
    public class SignalConfiguration : IEntityTypeConfiguration<Signal>
    {
        public void Configure(EntityTypeBuilder<Signal> builder)
        {
            builder.ToTable("Signals");
            builder.HasKey(s => s.SignalId);

            builder.Property(s => s.SignalType)
                .IsRequired();

            builder.Property(s => s.Amplitude)
                .IsRequired();

            builder.Property(s => s.Frequency)
                .IsRequired();

            builder.Property(s => s.PointsCount)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.HasMany(s => s.Points) // Связь с точками сигнала.
                .WithOne(p => p.Signal) // Связь один ко многим (каждый сигнал может иметь несколько точек).
                .HasForeignKey(p => p.SignalId) // Внешний ключ в таблице SignalPoint.
                .OnDelete(DeleteBehavior.Cascade); // При удалении сигнала будут удаляться и точки.

        }
    }
}
