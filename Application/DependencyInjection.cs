using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Merchants;
using Application.Orders.Deposit;
using Domain.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GetMerchantInfoService>();
            services.AddScoped<CreateDepositOrderService>();
            services.AddScoped<QueryDepositOrderService>();

            return services;
        }
    }
}