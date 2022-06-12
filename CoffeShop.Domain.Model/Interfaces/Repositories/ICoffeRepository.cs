using CoffeeShop.Domain.Model.DTOs;
using CoffeeShop.Domain.Model.Entities;

namespace CoffeeShop.Domain.Model.Interfaces.Repositories;

public interface ICoffeRepository
{
    Task AddAsync(Coffee coffee);
    Task<Coffee> GetByIdAsync(int id);
    Task<List<Coffee>> GetAllAsync();
    Task UpdateAsync(int id, CoffeeDTO coffeeDTO);
    Task DeleteAsync(Coffee coffee);
}
