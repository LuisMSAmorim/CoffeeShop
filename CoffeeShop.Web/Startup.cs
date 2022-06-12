using CoffeeShop.Infrastructure.IoC;

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

        DependencyInjectorHelper.Register(services, Configuration);
    }
}
