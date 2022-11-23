using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.HttpClientWrapper
{
    public class HttpRequestModel
    {
        public string ClientName { get; set; }
        public Uri Uri { get; set; }

        public string ContentType { get; set; }

        public Encoding Encoding { get; set; }
        public string RequestBody { get; set; }
    }
}