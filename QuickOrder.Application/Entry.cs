using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickOrder.Application.Services;
using QuickOrder.Domain.Interfaces;

namespace QuickOrder.Application
{
    public static class Entry
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
