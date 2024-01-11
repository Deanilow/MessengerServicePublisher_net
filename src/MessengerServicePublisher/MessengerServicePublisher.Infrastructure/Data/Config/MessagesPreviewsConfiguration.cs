using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class MessagesPreviewsConfiguration : BaseEntityConfiguration<MessagesPreviews>
    {
        public override void Configure(EntityTypeBuilder<MessagesPreviews> builder)
        {
            base.Configure(builder);

            builder.ToTable("MessagesPreviews");
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.To).HasMaxLength(20).IsRequired();
            builder.Property(entity => entity.From).HasMaxLength(20).IsRequired(false);
            builder.Property(entity => entity.Text).HasMaxLength(int.MaxValue).IsRequired();
            builder.Property(entity => entity.FileUrl).HasMaxLength(int.MaxValue).IsRequired(false);
        }
    }
}
