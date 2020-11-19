﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.MSSqlServer;

namespace WebFrameWork.Logging
{
   public static class LoggingConfiguration
   {
       public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, configuration) =>
       {
           #region Enriching Logger Context

           var env = context.HostingEnvironment;


           configuration.Enrich.FromLogContext()
               .Enrich.WithProperty("ApplicationName", env.ApplicationName)
               .Enrich.WithProperty("Environment", env.EnvironmentName)
               .Enrich.WithExceptionDetails();



           #endregion


           var columnOpts = new ColumnOptions();
           columnOpts.Store.Remove(StandardColumn.Properties);
           columnOpts.Store.Add(StandardColumn.LogEvent);
           columnOpts.LogEvent.DataLength = 4096;
           columnOpts.PrimaryKey = columnOpts.Id;
           columnOpts.Id.DataType = SqlDbType.Int;


           configuration.WriteTo
               .MSSqlServer(context.Configuration
                   .GetConnectionString("SqlServer"), "SystemLogs", autoCreateSqlTable: true)
               .MinimumLevel.Warning();

           configuration.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day);
           configuration.WriteTo.Console().MinimumLevel.Information();

           #region ElasticSearch Configuration. Comment if Not Needed 


           var elasticUrl = context.Configuration.GetValue<string>("Logging:ElasticUrl");

           if (!string.IsNullOrEmpty(elasticUrl))
           {
               configuration.WriteTo.Elasticsearch(
                   new ElasticsearchSinkOptions(new Uri(elasticUrl))
                   {
                       AutoRegisterTemplate = true,
                       AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                       IndexFormat = "web-logs-{0:yyyy.MM.dd}",
                       MinimumLogEventLevel = LogEventLevel.Debug
                   });
           }

           #endregion
       };
   }
}
