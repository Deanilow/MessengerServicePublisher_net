using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.HasQueryFilter(entity => entity.Deleted == null);
            builder.Property(entity => entity.Id);
            builder
                .Property(entity => entity.Created)
                .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc));
            builder
                .Property(entity => entity.Udpated)
                .HasConversion(value => value, value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null);
            builder
                .Property(entity => entity.Deleted)
                .HasConversion(value => value, value => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null);
        }
    }
}
