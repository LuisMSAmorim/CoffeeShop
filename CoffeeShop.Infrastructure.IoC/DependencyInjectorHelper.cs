using CoffeeShop.Domain.Model.Interfaces.Repositories;
using CoffeeShop.Domain.Model.Interfaces.Services.Domain;
using CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;
using CoffeeShop.Domain.Services.Services;
using CoffeeShop.Infrastructure.Data.Context;
using CoffeeShop.Infrastructure.Data.Repositories;
using CoffeeShop.Infrastructure.Services.Blob;
using CoffeeShop.Infrastructure.Services.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Infrastructure.IoC;

public sealed class DependencyInjectorHelper
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        DbContexts(services, configuration);

        Repositories(services);

        DomainServices(services);

        InfrastructureServices(services, configuration);
    }

    private static void DbContexts(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CoffeeShopDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
    }

    private static void Repositories(IServiceCollection services)
    {
        services.AddScoped<ICoffeRepository, CoffeeRepository>();
    }

    private static void DomainServices(IServiceCollection services)
    {
        services.AddScoped<ICoffeeService, CoffeeService>();
    }

    private static void InfrastructureServices(IServiceCollection services, IConfiguration configuration)
    {
        var storageAccountConnectionString = configuration.GetConnectionString("StorageAccount");

        services.AddScoped<IBlobService, BlobService>(provider => 
            new BlobService(storageAccountConnectionString));

        services.AddScoped<IFunctionService, FunctionService>(provider =>
            new FunctionService(configuration.GetConnectionString("FunctionBaseAddress")));
    }
}
