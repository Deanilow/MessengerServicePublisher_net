using MessengerServicePublisher.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessengerServicePublisher.Worker
{

    public class TimerChangeWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TimerChangeWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Obtener la cadena de conexión
            string connectionString;
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                connectionString = dbContext.Database.GetDbConnection().ConnectionString;
            }

            // Iniciar la escucha de notificaciones de cambio
            SqlDependency.Start(connectionString);

            // Configurar la dependencia de SQL
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM [MessengerService3].[wsp].[MessagesPreviews]", connection))
                {
                    // Configurar la notificación de cambio
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += (sender, e) => SqlDependency_OnChange(sender, e);

                    // Ejecutar la consulta para iniciar la escucha
                    command.ExecuteNonQuery();
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                // Lógica de trabajo del Worker Service
                // ...

                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            // Obtener la cadena de conexión
            string connectionString;
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                connectionString = dbContext.Database.GetDbConnection().ConnectionString;
            }

            // Detener la escucha de notificaciones de cambio
            SqlDependency.Stop(connectionString);

            await base.StopAsync(stoppingToken);
        }

        private void SqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            // Se ha producido un cambio en la tabla
            Console.WriteLine("Cambio detectado en la tabla");

            // Puedes realizar acciones según el cambio detectado
            // ...

            // Reiniciar la escucha de cambios si es necesario
            // StartListening();
        }
    }
}
