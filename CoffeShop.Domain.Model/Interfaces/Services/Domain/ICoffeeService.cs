using CoffeeShop.Domain.Model.DTOs;
using CoffeeShop.Domain.Model.Entities;

namespace CoffeeShop.Domain.Model.Interfaces.Services.Domain;

public interface ICoffeeService
{
    Task<List<Coffee>> GetAllAsync();
    Task<Coffee> GetByIdAsync(int id, bool SendVisualizationMessage);
    Task CreateAsync(Coffee coffee, Stream stream);
    Task UpdateAsync(int id, CoffeeDTO coffeeDTO, Stream stream);
    Task DeleteAsync(Coffee coffee);
}
