using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CoffeeShop.Infrastructure.Data.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoffeeShopDbContext>
{
    public CoffeeShopDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory()
            + "/../CoffeeShop.Web/appsettings.json").Build();

        var builder = new DbContextOptionsBuilder<CoffeeShopDbContext>();
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        builder.UseSqlServer(connectionString);

        return new CoffeeShopDbContext(builder.Options);
    }
}
