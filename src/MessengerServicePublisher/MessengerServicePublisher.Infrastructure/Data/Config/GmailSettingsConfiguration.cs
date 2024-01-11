using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class GmailSettingConfiguration : BaseEntityConfiguration<GmailSettings>
    {
        public override void Configure(EntityTypeBuilder<GmailSettings> builder)
        {
            base.Configure(builder);

            builder.ToTable("GmailSettings");
            builder.Property(entity => entity.Company).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Definition).HasMaxLength(500).IsRequired();
            builder.Property(entity => entity.Description).HasMaxLength(int.MaxValue).IsRequired();
            builder.Property(entity => entity.Type).IsRequired();
        }
    }
}
