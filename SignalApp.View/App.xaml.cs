using Microsoft.Extensions.DependencyInjection;
using SignalApp.Domain.Interfaces;
using SignalApp.Infrastructure.Database;
using SignalApp.Infrastructure.DI;
using SignalApp.Infrastructure.Services;
using System.IO;
using System.Windows;

namespace SignalApp.View
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        static App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPathProviderService, PathProviderService>();
            using var temp = services.BuildServiceProvider();
            var pathProviderService = temp.GetRequiredService<IPathProviderService>();

            string dbPath = Path.Combine(pathProviderService.GetBaseDirectory(), "signals.db");
            string connectionString = $"Data Source={dbPath}";

            services.AddInfrastrucutre(connectionString);
            services.AddApplicationServices();

            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = ServiceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SignalDbContext>();
                db.Database.EnsureCreated();
            }

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
