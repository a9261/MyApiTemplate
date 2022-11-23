using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLayer.Models;
using Domain.ExtensionMethod;
using Infrastructure.Service;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace ApiLayer.LogExtension
{
    public class MyAzureStorageSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly AzureTableService _azureTableService;

        public MyAzureStorageSink(IFormatProvider formatProvider, AzureTableService azureTableService)
        {
            _formatProvider = formatProvider;
            _azureTableService = azureTableService;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            if (logEvent.Properties.ContainsKey("LogLabel"))
            {
                var tableName = $"PaoFenPaymentPublicApiRequestLog{DateTime.UtcNow:yyyyMMdd}";
                _azureTableService.CreateTableIfNotExists(tableName);

                var logItem = new ApiRequestLogToAzureTable()
                {
                    Host = logEvent.Properties["Host"].ToString().ToCleanString("\""),
                    Method = logEvent.Properties["Method"].ToString().ToCleanString("\""),
                    ContentType = logEvent.Properties["ContentType"].ToString().ToCleanString("\""),
                    QueryString = logEvent.Properties["QueryString"].ToString().ToCleanString("\""),
                    ResponseBody = logEvent.Properties["ResponseBody"].ToString().ToCleanString("\\"),
                };
                if (logEvent.Properties["Method"].ToString().ToCleanString("\"") == "POST")
                {
                    logItem.RequestBody = logEvent.Properties["RequestBody"].ToString().ToCleanString("\\");

                    if (logEvent.Properties.ContainsKey("ErrorMessage"))
                    {
                        logItem.Error = logEvent.Properties["ErrorMessage"].ToString();
                    }
                    if (logEvent.Properties.ContainsKey("ErrorInnerMessage"))
                    {
                        logItem.ErrorInnerMessage = logEvent.Properties["ErrorInnerMessage"].ToString();
                    }

                    _azureTableService.InsertEntity(tableName, logItem);
                }
            }
        }
    }

    public static class MyAzureStorageSinkExtensions
    {
        public static LoggerConfiguration MyAzureStorageSink(
            this LoggerSinkConfiguration loggerConfiguration,
            AzureTableService azureTableService, IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new MyAzureStorageSink(formatProvider, azureTableService));
        }
    }
}