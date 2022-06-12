using CoffeeShop.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Data.Context;

public class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options) { }

    public virtual DbSet<Coffee> Coffee { get; set; }
}
