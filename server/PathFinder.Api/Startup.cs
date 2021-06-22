using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private bool IsDevelopment { get; set; }
        public IContainer ApplicationContainer { get; private set; }
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AllowAllCors(MyAllowSpecificOrigins);

            services.RegisterConfigurations(Configuration);
            services.RegisterDependencies();
            services.RegisterDatabase(GetHerokuConnectionString());
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataAccessMappingProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
            
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
            builder.RegisterAlgorithmsAndRenders();
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);

            #endregion
        }

        private string GetHerokuConnectionString()
        {
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(connectionUrl);
            var db = databaseUri.LocalPath.TrimStart('/');
            var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
        
        public string DatabaseConnectionString =>
            IsDevelopment
                ? Configuration.GetConnectionString("DefaultConnection")
                : GetHerokuConnectionString();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IsDevelopment = env.IsDevelopment();
            
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
                    spa.UseReactDevelopmentServer("start");
            });
        }

       
    }
}