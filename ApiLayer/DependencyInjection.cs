using ApiLayer.ControllerFilters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<UnBoxingRequestFilter>();
            return services;
        }
    }
}