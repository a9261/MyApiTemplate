using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace Infrastructure.Models
{
    public class BaseAzureTableLog : ITableEntity
    {
        public string PartitionKey { get; set; } = DateTime.UtcNow.ToString("yyyyMMddHH");

        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}