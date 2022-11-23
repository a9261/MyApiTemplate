using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace ApiLayer.Models
{
    public class ApiRequestLogToAzureTable : BaseAzureTableLog
    {
        public string Host { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string QueryString { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string Error { get; set; }
        public string ErrorInnerMessage { get; set; }
    }
}