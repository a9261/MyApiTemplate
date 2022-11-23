using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace ApiLayer.LogExtension
{
    public static class SerilogEnrichTemplate
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            diagnosticContext.Set("LogLabel", "HTTP Request");
            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Scheme + "://" + request.Host);
            diagnosticContext.Set("Method", request.Method);
            diagnosticContext.Set("RequestBody", "");
            diagnosticContext.Set("ResponseBody", "");
            diagnosticContext.Set("QueryString", "");
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }
            //RequestBody has Data
            //This body is processed by action filter
            if (request.Body.CanSeek)
            {
                request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                var body = new System.IO.StreamReader(request.Body).ReadToEnd();
                diagnosticContext.Set("RequestBody", body);
                request.Body.Seek(0, System.IO.SeekOrigin.Begin);
            }

            diagnosticContext.Set("ContentType", request.ContentType);

            var response = httpContext.Response;
            if (response.Body.CanSeek)
            {
                response.Body.Seek(0, System.IO.SeekOrigin.Begin);
                var responsebody = new System.IO.StreamReader(response.Body).ReadToEnd();
                diagnosticContext.Set("ResponseBody", responsebody);
                response.Body.Seek(0, System.IO.SeekOrigin.Begin);
            }

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }
    }
}