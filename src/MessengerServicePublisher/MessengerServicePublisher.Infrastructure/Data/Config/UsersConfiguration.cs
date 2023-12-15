using MessengerServicePublisher.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerServicePublisher.Infrastructure.Data.Config
{
    public class UsersConfiguration : BaseEntityConfiguration<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");
            builder.HasIndex(entity => entity.Email).IsUnique().HasDatabaseName("IX_User_Email");
            builder.Property(entity => entity.Name).HasMaxLength(100).IsRequired();
            builder.Property(entity => entity.Email).HasMaxLength(120).IsRequired();
            builder.Property(entity => entity.Password).HasMaxLength(300).IsRequired();
        }
    }
}
