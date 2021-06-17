using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathFinder.Api.Models;
using PathFinder.DataAccess1;
using PathFinder.DataAccess1.Implementations.MySQL;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.MazeCreation;
using PathFinder.Domain.Models.MazeCreation.MazeGenerators;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Services.MazeService;
using PathFinder.Infrastructure.PriorityQueue;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPriorityQueue<Point>, HeapPriorityQueue<Point>>();
            services.AddScoped<IPriorityQueueProvider<Point>, PriorityQueueProvider<Point>>();
            
            services.AddScoped<IMazeRepository, MySqlRepository>();
            services.AddScoped<IMazeService, MazeService>();
            services.AddSingleton<IMetricFactory, MetricFactory>();

            services.AddScoped<IMazeGenerator, Kruskal>();

            services.AddScoped<SettingsProvider>();
            services.AddSingleton<AlgorithmsHandler>();
            services.AddSingleton<IAlgorithmsExecutor, AlgorithmsExecutor>();

            services.AddSingleton<IMazeCreationFactory, MazeCreationFactory>();
        }

        public static void RegisterConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ => new GridConfigurationParameters
            {
                Width = int.Parse(configuration["GridParameters:Width"]),
                Height = int.Parse(configuration["GridParameters:Height"])
            });
        }

        public static void AllowAllCors(this IServiceCollection services, string originsName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: originsName,
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });
        }

        public static void RegisterDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MazeContext>(opt =>
                opt.UseNpgsql(connectionString));
        }
    }
}