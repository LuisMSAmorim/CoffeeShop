using CoffeeShop.Domain.Model.DTOs;
using CoffeeShop.Domain.Model.Entities;
using CoffeeShop.Domain.Model.Interfaces;
using CoffeeShop.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Data.Repositories;

public sealed class CoffeesRepository : ICoffesRepository
{
    private readonly CoffeeShopDbContext _context;

    public async Task AddAsync(Coffee coffee)
    {
        await _context.AddAsync(coffee);
    }

    public async Task DeleteAsync(Coffee coffee)
    {
        _context.Remove(coffee);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Coffee>> GetAllAsync()
    {
        return await _context.Coffee.ToListAsync();
    }

    public async Task<Coffee> GetByIdAsync(int id)
    {
        return await _context.Coffee
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(int id, CoffeeDTO coffeeDTO)
    {
        var currentCoffee = await _context.Coffee
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        _context.Entry(currentCoffee).CurrentValues.SetValues(coffeeDTO);

        await _context.SaveChangesAsync();
    }
}
