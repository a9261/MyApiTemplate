using ApiLayer.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiLayer.ControllerFilters
{
    public class BoxingResponseFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is Microsoft.AspNetCore.Mvc.ObjectResult)
            {
                var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
                var response = new CustomerApiResponse();
                response.MerchantNo = "GBP99999";
                response.Response = Newtonsoft.Json.JsonConvert.SerializeObject(result.Value);
                response.SignMsg = "sign";
                context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult(response);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}