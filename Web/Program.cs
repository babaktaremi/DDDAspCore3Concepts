using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Identity.SeedDatabaseService;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var webHost= CreateHostBuilder(args).Build();

            #region Seeding Database

            var scope = webHost.Services.CreateScope();

            using (scope)
            {
                var seedService = scope.ServiceProvider.GetRequiredService<ISeedDataBase>();

                await seedService.Seed();
            }

            await webHost.StartAsync();

            #endregion
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();


                    #region Serilog Configuration

                    webBuilder.UseSerilog((webHostBuilderContext, logger) =>
                    {
                       

                        var columnOpts = new ColumnOptions();
                        columnOpts.Store.Remove(StandardColumn.Properties);
                        columnOpts.Store.Add(StandardColumn.LogEvent);
                        columnOpts.LogEvent.DataLength = 4096;
                        columnOpts.PrimaryKey = columnOpts.Id;
                        columnOpts.Id.DataType = SqlDbType.Int;


                        logger.WriteTo
                            .MSSqlServer(webHostBuilderContext.Configuration
                                .GetConnectionString("SqlServer"), "SystemLogs", autoCreateSqlTable: true)
                            .MinimumLevel.Warning();

                        logger.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day);
                        logger.WriteTo.Console().MinimumLevel.Information();
                    });

                    #endregion
                });
    }
}
