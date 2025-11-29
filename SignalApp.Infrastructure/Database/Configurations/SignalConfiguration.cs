using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalApp.Domain.Models;

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
        }
    }
}
