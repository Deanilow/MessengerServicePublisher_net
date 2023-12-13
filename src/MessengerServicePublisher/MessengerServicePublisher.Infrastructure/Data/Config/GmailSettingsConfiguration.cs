using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class GmailSettingConfiguration : BaseEntityConfiguration<GmailSetting>
    {
        public override void Configure(EntityTypeBuilder<GmailSetting> builder)
        {
            base.Configure(builder);

            builder.ToTable("GmailSetting");
            builder.Property(entity => entity.Company).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Definition).HasMaxLength(120).IsRequired();
            builder.Property(entity => entity.Description).HasMaxLength(int.MaxValue).IsRequired();
        }
    }
}
