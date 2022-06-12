using CoffeeShop.Domain.Model.Entities;

namespace CoffeeShop.Domain.Model.Interfaces.Services.Domain;

public interface ICoffeeService
{
    Task<List<Coffee>> GetAllAsync();
    Task<Coffee> GetByIdAsync(int id);
    Task CreateAsync(Coffee coffee);
    Task UpdateAsync(int id, Coffee coffee);
    Task DeleteAsync(Coffee coffee);
}
