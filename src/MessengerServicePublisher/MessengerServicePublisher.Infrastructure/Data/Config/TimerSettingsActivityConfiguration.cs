using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class TimerSettingsActivityConfiguration : BaseEntityConfiguration<TimerSettingsActivity>
    {
        public override void Configure(EntityTypeBuilder<TimerSettingsActivity> builder)
        {
            base.Configure(builder);

            builder.ToTable("TimerSettingsActivity");

            builder.Property(e => e.IdTimerSettings)
                .IsRequired();
        }
    }
}
