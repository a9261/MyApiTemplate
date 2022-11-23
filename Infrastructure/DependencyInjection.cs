using Domain.Merchant;
using Domain.Merchants;
using Domain.Orders.Deposit;
using Infrastructure.Models;
using Infrastructure.Repository.Common;
using Infrastructure.Repository.Merchant;
using Infrastructure.Repository.Orders.Deposit;
using Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("default", httpClient =>
            {
                int timeout = 180;
                int.TryParse(configuration["Setting:HttpClientRequestTimeoutSecond"], out timeout);
                httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            });

            services.AddScoped<HttpClientWrapper>();
            services.AddScoped<AzureTableService>();
            services.AddScoped<DbPaofenContext>();
            services.AddScoped<BaseQueryRepository>();

            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IDepositOrderRepository, DepositOrderRepository>();

            return services;
        }
    }
}