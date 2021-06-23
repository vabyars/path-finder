using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathFinder.Api.Models;
using PathFinder.DataAccess;
using PathFinder.DataAccess.Implementations;
using PathFinder.DataAccess.Implementations.Database;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.Algorithms.AlgorithmsExecutor;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.MazeCreation;
using PathFinder.Domain.Models.MazeCreation.MazeGenerators;
using PathFinder.Domain.Services.MazeService;
using PathFinder.Infrastructure.PriorityQueue;
using PathFinder.Infrastructure.PriorityQueue.Realizations;

namespace PathFinder.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPriorityQueueProvider<Point, IPriorityQueue<Point>>,
                PriorityQueueProvider<Point, HeapPriorityQueue<Point>>>();
            
            //services.AddSingleton<IMazeRepository, MazeRepository>();
            services.AddScoped<IMazeRepository, DatabaseRepository>();
            services.AddScoped<IMazeService, MazeService>();

            services.AddScoped<IMazeGenerator, Kruskal>();

            services.AddScoped<SettingsProvider>();
            services.AddScoped<IAlgorithmsHandler, AlgorithmsHandler>();
            services.AddScoped<IAlgorithmsExecutor, AlgorithmsExecutor>();

            services.AddScoped<IMazeCreationFactory, MazeCreationFactory>();
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
                opt.UseSqlServer(connectionString));
        }
    }
}