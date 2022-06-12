using CoffeeShop.Domain.Model.Interfaces.Repositories;
using CoffeeShop.Domain.Model.Interfaces.Services;
using CoffeeShop.Domain.Services.Services;
using CoffeeShop.Infrastructure.Data.Context;
using CoffeeShop.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Infrastructure.IoC;

public sealed class DependencyInjectorHelper
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CoffeeShopDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddScoped<ICoffeRepository, CoffeeRepository>();
        services.AddScoped<ICoffeeService, CoffeeService>();
    }
}
