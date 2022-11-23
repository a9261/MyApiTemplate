using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiLayer.Models;
using Domain.Environment.Model;
using Domain.Environment.Model.Enums;
using Domain.ExtensionMethod;
using Newtonsoft.Json;
using Serilog;
using Serilog.Extensions.Hosting;

namespace ApiLayer.MiddleWares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDiagnosticContext diagnosticContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, diagnosticContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IDiagnosticContext diagnosticContext)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var returnResponse = new CustomerApiResponse()
            {
                //MerchantNo = context.User.Identity?.Name,
            };
            var errorResponse = new ApiErrorResponse();

            switch (exception)
            {
                case CustomerSystemException ex:
                    if (exception.Data.Contains("ResponseCode"))
                    {
                        errorResponse.ResponseCode = (SystemResponseCodeEnum)exception.Data["ResponseCode"];
                        errorResponse.Message = ((SystemResponseCodeEnum)exception.Data["ResponseCode"]).GetDescription();

                        diagnosticContext.Set("ErrorMessage", ((SystemResponseCodeEnum)exception.Data["ResponseCode"]).GetDescription());
                    }
                    else
                    {
                        errorResponse.ResponseCode = SystemResponseCodeEnum.SystemError;
                        errorResponse.Message = SystemResponseCodeEnum.SystemError.GetDescription();
                        diagnosticContext.Set("ErrorMessage", SystemResponseCodeEnum.SystemError.GetDescription());
                    }
                    diagnosticContext.Set("ErrorInnerMessage", ex.InnerException?.Message);
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ResponseCode = SystemResponseCodeEnum.SystemError;
                    errorResponse.Message = "SystemError";
                    diagnosticContext.Set("ErrorMessage", exception.Message);
                    diagnosticContext.Set("ErrorInnerMessage", exception.InnerException?.Message);
                    break;
            }
            _logger.LogError(exception.Message);
            returnResponse.Response = JsonConvert.SerializeObject(errorResponse);
            var result = JsonConvert.SerializeObject(returnResponse);
            await context.Response.WriteAsync(result);
        }
    }
}