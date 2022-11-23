using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models.HttpClientWrapper;

namespace Infrastructure.Service
{
    public class HttpClientWrapper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseModel> Get(HttpRequestModel request)
        {
            var client = GetClient(request.ClientName);

            var requestEncoding = request.Encoding ?? Encoding.UTF8;
            var response = new HttpResponseModel();
            var httpResponse = await client.GetAsync(request.Uri);

            byte[] byteArray = await httpResponse.Content.ReadAsByteArrayAsync();
            response.RawData = requestEncoding.GetString(byteArray, 0, byteArray.Length);

            response.HttpStatusCode = httpResponse.StatusCode;
            if (httpResponse.Headers.Any())
            {
                response.Headers = httpResponse.Headers.ToDictionary(k => k.Key, v => v.Value);
            }

            return response;
        }

        public async Task<HttpResponseModel> Post(HttpRequestModel request)
        {
            var client = GetClient(request.ClientName);

            var requestEncoding = request.Encoding ?? Encoding.UTF8;
            var sendContent = new StringContent(request.RequestBody ?? "", requestEncoding, request.ContentType);

            var response = new HttpResponseModel();
            var httpResponse = await client.PostAsync(request.Uri, sendContent);

            byte[] byteArray = await httpResponse.Content.ReadAsByteArrayAsync();
            response.RawData = requestEncoding.GetString(byteArray, 0, byteArray.Length);

            response.HttpStatusCode = httpResponse.StatusCode;
            if (httpResponse.Headers.Any())
            {
                response.Headers = httpResponse.Headers.ToDictionary(k => k.Key, v => v.Value);
            }

            return response;
        }

        private HttpClient GetClient(string clientName)
        {
            HttpClient client;
            if (string.IsNullOrEmpty(clientName))
            {
                client = _httpClientFactory.CreateClient("default");
            }
            else
            {
                client = _httpClientFactory.CreateClient(clientName);
            }

            return client;
        }
    }
}