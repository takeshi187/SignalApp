using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SignalApp.ApplicationServices.Interfaces;
using SignalApp.ApplicationServices.Services;
using SignalApp.ApplicationServices.Services.Generators;
using SignalApp.Domain.Interfaces;
using SignalApp.Infrastructure.Database;
using SignalApp.Infrastructure.Repositories;
using SignalApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalApp.Infrastructure.DI
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastrucutre(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<SignalDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<ISignalRepository, SignalRepository>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<ISignalService, SignalService>();
            services.AddScoped<ISignalValidator, SignalValidator>();
            services.AddScoped<ISignalProcessor,  SignalProcessor>();

            services.AddScoped<ISignalGenerator, SinusGenerator>();
            services.AddScoped<ISignalGenerator, SquareGenerator>();
            services.AddScoped<ISignalGenerator, TriangleGenerator>();
            services.AddScoped<ISignalGenerator, SawtoothGenerator>();

            return services;
        }
    }
}
