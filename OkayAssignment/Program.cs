using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace OkayAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ------------- Adding Serilog -------------
            Log.Logger = new LoggerConfiguration()
                // .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\AspCoreWebApplication-log-.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            // ------------- Adding Serilog -------------

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                 
                }).UseSerilog();
    }
}
