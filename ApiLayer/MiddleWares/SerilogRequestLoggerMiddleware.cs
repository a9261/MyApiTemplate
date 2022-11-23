using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLayer.MiddleWares
{
    public class SerilogRequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogRequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDiagnosticContext diagnosticContext)
        {
            var request = httpContext.Request;
            request.EnableBuffering();

            diagnosticContext.Set("LogLabel", "HTTP Request");
            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Scheme + "://" + request.Host);
            diagnosticContext.Set("Method", request.Method);
            diagnosticContext.Set("ContentType", request.ContentType);

            diagnosticContext.Set("RequestBody", "");
            diagnosticContext.Set("ResponseBody", "");
            diagnosticContext.Set("QueryString", "");
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            if (request.Body.CanSeek)
            {
                request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                var body = await (new System.IO.StreamReader(request.Body)).ReadToEndAsync();
                diagnosticContext.Set("RequestBody", body);
                request.Body.Seek(0, System.IO.SeekOrigin.Begin);
            }

            var originalRespBody = httpContext.Response.Body;
            using (var newResponseBody = new MemoryStream())
            {
                httpContext.Response.Body = newResponseBody;

                await _next(httpContext);

                newResponseBody.Seek(0, System.IO.SeekOrigin.Begin);
                //newResponseBody.Position = 0;

                if (newResponseBody.CanSeek)
                {
                    newResponseBody.Seek(0, System.IO.SeekOrigin.Begin);
                    var responseBody = await (new System.IO.StreamReader(newResponseBody)).ReadToEndAsync();
                    diagnosticContext.Set("ResponseBody", responseBody);
                    newResponseBody.Seek(0, System.IO.SeekOrigin.Begin);
                }

                await newResponseBody.CopyToAsync(originalRespBody);
                httpContext.Response.Body = originalRespBody;
            }
        }
    }
}