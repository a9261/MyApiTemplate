using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.HttpClientWrapper
{
    public class HttpResponseModel
    {
        public Dictionary<string, IEnumerable<string>> Headers { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public string RawData { get; set; }
    }
}