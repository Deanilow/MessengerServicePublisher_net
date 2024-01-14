using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class TimerSettingsConfiguration : BaseEntityConfiguration<TimerSettings>
    {
        public override void Configure(EntityTypeBuilder<TimerSettings> builder)
        {
            base.Configure(builder);

            builder.ToTable("TimerSettings");
            builder.Property(entity => entity.Company).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Definition).HasMaxLength(500).IsRequired();

            builder.Property(e => e.HourStart)
            .IsRequired()
            .HasMaxLength(8)
            .HasConversion(
                v => TimeSpan.ParseExact(v, "hh\\:mm\\:ss", null),
                v => v.ToString("hh\\:mm\\:ss"));

            builder.Property(e => e.HourMax)
                .IsRequired(false)
                .HasMaxLength(8)
                .HasConversion(
                    v => TimeSpan.ParseExact(v, "hh\\:mm\\:ss", null),
                    v => v.ToString("hh\\:mm\\:ss"))
                .HasComputedColumnSql("DATEADD(MINUTE, 10, CONVERT(time, [HourStart]))");

            builder.Property(e => e.DateStart)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasConversion(
                    v => DateTime.ParseExact(v, "dd-MM-yyyy", null),
                    v => v.ToString("dd-MM-yyyy"));
        }
    }
}
