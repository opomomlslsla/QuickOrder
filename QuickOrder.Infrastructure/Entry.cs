using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Models;
using QuickOrder.Infrastructure.Data;
using QuickOrder.Infrastructure.Repositories;

namespace QuickOrder.Infrastructure;

public static class Entry
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IRepository<Order>, OrderRepository>();
        return services;
    }
}