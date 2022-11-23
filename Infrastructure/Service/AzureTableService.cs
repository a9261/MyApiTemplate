using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Service
{
    public class AzureTableService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        private readonly int _keepDataDays = 90;
        private readonly bool _isRemoveHistoryEnabled = false;

        public AzureTableService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(configuration["Setting:Log:LoggingAzureTableConnectionString"]);

            //TODO : 資料自動清除
            if (!string.IsNullOrEmpty(configuration["Setting:Log:KeepDataDays"]))
            {
                _keepDataDays = int.Parse(configuration["Setting:Log:KeepDataDays"]);
            }
            if (!string.IsNullOrEmpty(configuration["Setting:Log:IsRemoveHistoryEnabled"]))
            {
                _isRemoveHistoryEnabled = int.Parse(configuration["Setting:Log:IsRemoveHistoryEnabled"]) == 1;
            }
        }

        public void CreateTableIfNotExists(string tableName)
        {
            _tableServiceClient.CreateTableIfNotExists(tableName);
        }

        public void InsertEntity(string tableName, ITableEntity entity)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            tableClient.AddEntity(entity);
        }

        public void DeleteEntity(string tableName)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            tableClient.Delete();
        }

        public Task<Response<TableItem>> CreateTableIfNotExistsAsync(string tableName)
        {
            return _tableServiceClient.CreateTableIfNotExistsAsync(tableName);
        }

        public Task<Response> InsertEntityAsync(string tableName, ITableEntity entity)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            return tableClient.AddEntityAsync(entity);
        }

        public Task<Response> DeleteEntityAsync(string tableName)
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            return tableClient.DeleteAsync();
        }
    }
}