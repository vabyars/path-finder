using System;
using System.Drawing;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PathFinder.DataAccess1;
using PathFinder.DataAccess1.Implementations;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Services;
using PathFinder.Infrastructure;
using PathFinder.Infrastructure.Interfaces;

namespace PathFinder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            
            services.AddTransient<IPriorityQueue<Point>, DictionaryPriorityQueue<Point>>(); // TODO Fix
            services.AddSingleton<IMazeRepository, MazeRepository>();
            services.AddSingleton<IMazeService, MazeService>();
            services.AddSingleton<IMazeCreationFactory, MazeCreationFactoryTestRealization>();

            services.AddTransient<IAlgorithm<State>, AStarAlgorithm>();
            
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "PathFinder.Api",
                    Version = "v1",
                });
            });

            #region Autofac injection

            var builder = new ContainerBuilder(); //done to allow sequence injection
            builder.Populate(services);
            builder.RegisterType<AlgorithmsExecutor>().As<IAlgorithmsExecutor>();
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PathFinder.Api v1"));
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}