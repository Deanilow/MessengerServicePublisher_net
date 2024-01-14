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
                .Property(entity => entity.Created);
            builder
                .Property(entity => entity.Udpated);
            builder
                .Property(entity => entity.Deleted);
        }
    }
}
