using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PathFinder.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls($"http://*:{HostPort}")
                .UseStartup<Startup>();
        
        private static bool IsLocal =>
            Environment.GetEnvironmentVariable("PROGRAM_ENVIRONMENT") == "local";
        
        public static string HostPort =>
            IsLocal
                ? "5000"
                : Environment.GetEnvironmentVariable("PORT");
    }
}