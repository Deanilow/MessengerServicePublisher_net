using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class MessagesConfiguration : BaseEntityConfiguration<Messages>
    {
        public override void Configure(EntityTypeBuilder<Messages> builder)
        {
            base.Configure(builder);

            builder.ToTable("Messages");
            builder.Property(entity => entity.To).HasMaxLength(20).IsRequired();
            builder.Property(entity => entity.From).HasMaxLength(20).IsRequired();
            builder.Property(entity => entity.MessagesDetail).HasMaxLength(int.MaxValue).IsRequired();
            builder.Property(entity => entity.StatusDescription).HasMaxLength(2000).IsRequired(false);
        }
    }
}
