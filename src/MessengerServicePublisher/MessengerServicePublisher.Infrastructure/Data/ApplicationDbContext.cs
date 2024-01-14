using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MessengerServicePublisher.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<GmailSettings> GmailSettings { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<MessagesPreviews> MessagesPreviews { get; set; }
        //public virtual DbSet<TimerSettings> TimerSettings { get; set; }
        //public virtual DbSet<TimerSettingsActivity> TimerSettingsActivity { get; set; }

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

            builder.Entity<GmailSettings>().ToTable("GmailSettings", "wsp");
            builder.Entity<Messages>().ToTable("Messages", "wsp");
            builder.Entity<MessagesPreviews>().ToTable("MessagesPreviews", "wsp");
            //builder.Entity<TimerSettings>().ToTable("TimerSettings", "wsp");
            //builder.Entity<TimerSettingsActivity>().ToTable("TimerSettingsActivity", "wsp");

            //builder.Entity<TimerSettingsActivity>()
            //    .HasOne(hijo => hijo.TimerSettings)
            //    .WithMany(padre => padre.TimerSettingsActivity)
            //    .HasForeignKey(hijo => hijo.IdTimerSettings);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
