using CoffeeShop.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Web;

public sealed class Startup
{
    public IConfigurationRoot Configuration { get; }

    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddDbContext<CoffeeShopDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

    }
}
