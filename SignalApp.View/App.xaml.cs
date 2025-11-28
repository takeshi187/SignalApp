using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalApp.Infrastructure.DI;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            string solutionFolder = FindSolutionFolder();

            string dbPath = Path.Combine(solutionFolder, "signals.db");

            string connectionString = $"Data Source={dbPath}";

            services.AddInfrastrucutre(connectionString);
            services.AddApplicationServices();

            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

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
