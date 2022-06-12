using CoffeeShop.Domain.Model.DTOs;
using CoffeeShop.Domain.Model.Entities;

namespace CoffeeShop.Domain.Model.Interfaces.Services;

public interface ICoffeeService
{
    Task<List<Coffee>> GetAllAsync();
    Task<Coffee> GetByIdAsync(int id);
    Task CreateAsync(Coffee coffee);
    Task UpdateAsync(int id, CoffeeDTO coffeeDTO);
    Task DeleteAsync(Coffee coffee);
}
