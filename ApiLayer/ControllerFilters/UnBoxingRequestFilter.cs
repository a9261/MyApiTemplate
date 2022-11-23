using System;
using System.Text;
using ApiLayer.Models;
using Domain.Validator;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;

namespace ApiLayer.ControllerFilters
{
    public class UnBoxingRequestFilter : Attribute, IAsyncResourceFilter
    {
        public UnBoxingRequestFilter()
        {
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "POST")
            {
                //Read Stream as string
                var stream = context.HttpContext.Request.Body;
                var streamReader = new System.IO.StreamReader(stream);
                var requestString = await streamReader.ReadToEndAsync();
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                //Deserialize to CustomerApiRequest
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerApiRequest>(requestString);

                context.HttpContext.Request.Body =
                    new System.IO.MemoryStream(Encoding.UTF8.GetBytes(request.Request));
            }
            await next();
        }
    }
}