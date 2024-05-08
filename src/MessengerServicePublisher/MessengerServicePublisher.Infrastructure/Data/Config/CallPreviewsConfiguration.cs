using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class CallPreviewsConfiguration : BaseEntityConfiguration<CallPreviews>
    {
        public override void Configure(EntityTypeBuilder<CallPreviews> builder)
        {
            base.Configure(builder);

            builder.ToTable("CallPreviews");
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.Company).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Definition).HasMaxLength(1000).IsRequired();
            builder.Property(entity => entity.To).HasMaxLength(20).IsRequired();
            builder.Property(entity => entity.From).HasMaxLength(20).IsRequired();
            builder.Property(entity => entity.FileUrl).HasMaxLength(1000).IsRequired();
            builder.Property(entity => entity.JsonRequest).HasMaxLength(int.MaxValue).IsRequired(false);
            builder.Property(entity => entity.JsonResponse).HasMaxLength(int.MaxValue).IsRequired(false);
        }
    }
}