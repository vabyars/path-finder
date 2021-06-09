using System;
using System.Drawing;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PathFinder.Api.Models;
using PathFinder.DataAccess1;
using PathFinder.DataAccess1.Implementations.MySQL;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.Algorithms.Lee;
using PathFinder.Domain.Models.MazeGenerators;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Renders;
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
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });

            services.AddSingleton(_ => new GridConfigurationParameters
            {
                Width = int.Parse(Configuration["GridParameters:Width"]),
                Height = int.Parse(Configuration["GridParameters:Height"])
            });
            
            services.AddScoped<IPriorityQueue<Point>, HeapPriorityQueue<Point>>();
            services.AddScoped<IPriorityQueueProvider<Point>, PriorityQueueProvider<Point>>();
            //services.AddSingleton<IMazeRepository, MazeRepository>();
            services.AddScoped<IMazeRepository, MySqlRepository>();
            services.AddScoped<IMazeService, MazeService>();
            services.AddScoped<IMetricFactory, MetricFactory>();

            //services.AddScoped<IAlgorithm<State>>(sp
              //  => new AStarAlgorithm(new AStarRenderNew(), sp.GetRequiredService<IPriorityQueueProvider<Point>>()));
            
            //services.AddTransient<IRender, AStarRenderNew>();
            //services.AddTransient<IAlgorithm<State>, AStarAlgorithm>();
            services.AddTransient<IAlgorithm, JpsDiagonal>();
            services.AddTransient<IAlgorithm, LeeAlgorithm>();
            services.AddTransient<IAlgorithm, IDA>();
            
            services.AddTransient<IMazeGenerator, Kruskal>();

            //services.AddTransient<Render, AStarRender>();
            //services.AddTransient<Render, LeeRender>();

            services.AddScoped<SettingsProvider>();
            services.AddScoped<DomainAlgorithmsController>();
            services.AddScoped<IAlgorithmsExecutor, AlgorithmsExecutor>();

            services.AddScoped<IMazeCreationFactory, MazeCreationFactory>();

            
            services.AddDbContext<MazeContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PathFinder.Api",
                    Version = "7.0",
                });
            });
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../ClientApp/build";
            });
            
            #region Autofac injection

            var builder = new ContainerBuilder(); //done to apply wheninjectedinto analog
            builder.Populate(services);
            RegisterAlgorithms(builder);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);

            #endregion
        }

        private void RegisterAlgorithms(ContainerBuilder builder)
        {
            builder.RegisterType<AStarRender>().Named<IRender>("AStar");
            builder.RegisterType<AStarAlgorithm>().As<IAlgorithm>()
                .WithParameter(ResolvedParameter.ForNamed<IRender>("AStar"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PathFinder.Api v1"));
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}