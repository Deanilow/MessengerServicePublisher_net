using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Infrastructure.Data;
using MessengerServicePublisher.Infrastructure.Data.Config;
using MessengerServicePublisher.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IBaseRepository, BaseRepository>();
        services.AddScoped<IGmailSettingRepository, GmailSettingRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();
        services.AddScoped<IMessagesPreviewsRepository, MessagesPreviewsRepository>();
        services.AddScoped<ICallPreviewsRepository, CallPreviewsRepository>();
        return services;
    }
}
