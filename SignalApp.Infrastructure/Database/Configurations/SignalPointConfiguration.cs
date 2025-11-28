using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Infrastructure.Database.Configurations
{
    public class SignalPointConfiguration : IEntityTypeConfiguration<SignalPoint>
    {
        public void Configure(EntityTypeBuilder<SignalPoint> builder)
        {
            builder.ToTable("SignalPoints");
            builder.HasKey(p => p.SignalPointId);

            builder.Property(p => p.Time)
                .IsRequired();

            builder.Property(p => p.Value)
                .IsRequired();

            builder
                .HasOne(p => p.Signal)
                .WithMany(s => s.Points)
                .HasForeignKey(p => p.SignalId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
