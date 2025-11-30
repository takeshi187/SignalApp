using Microsoft.Extensions.DependencyInjection;
using SignalApp.Infrastructure.Database;
using SignalApp.Infrastructure.DI;
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
            // для проекта
            string solutionFolder = FindSolutionFolder();

            // для exe 
            //string dbPath = Path.Combine(AppContext.BaseDirectory, "signals.db");

            // для проекта
            string dbPath = Path.Combine(solutionFolder, "signals.db");

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

        // для проекта
        private static string FindSolutionFolder()
        {
            string current = AppContext.BaseDirectory;

            while (!string.IsNullOrEmpty(current))
            {
                var slnFiles = Directory.GetFiles(current, "*.slnx");
                if (slnFiles.Length > 0)
                    return current;

                current = Directory.GetParent(current)?.FullName;
            }

            return AppContext.BaseDirectory;
        }
    }
}
