    using MessengerServicePublisher.Core.Entities;
    using MessengerServicePublisher.Infrastructure.Data.Config;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    namespace MessengerServicePublisher.Infrastructure.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public virtual DbSet<Users> Users { get; set; }
            public virtual DbSet<GmailSettings> GmailSettings { get; set; }
            public virtual DbSet<Messages> Messages { get; set; }

            private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

            public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
                : base(options)
            {
                _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

                base.OnModelCreating(builder);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
    }
