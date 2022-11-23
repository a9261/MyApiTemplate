using System;
using System.Text;
using ApiLayer.Models;
using Domain.Merchant.Models;
using Domain.Merchants;
using Domain.Validator;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;

namespace ApiLayer.ControllerFilters
{
    public class ValidMerchantRequestFilter : Attribute, IAsyncResourceFilter
    {
        public ValidMerchantRequestFilter()
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
                //Valid
                Valid(request, context);
            }
            await next();
        }

        public void Valid(CustomerApiRequest request, ResourceExecutingContext context)
        {
            var dbMerchantData = ((string)context.HttpContext.Items["Merchant"]).ConvertTo<Merchant>();

            MessageSignValidator.Valid(new MerchantRequest()
            {
                Content = request.Request,
                MerchantNo = request.MerchantNo,
                MessageSign = request.SignMsg,
                SignKey = dbMerchantData.SignKey
            });
        }
    }
}