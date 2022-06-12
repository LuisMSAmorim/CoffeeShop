using CoffeeShop.Domain.Model.Entities;
using CoffeeShop.Domain.Model.Interfaces.Repositories;
using CoffeeShop.Domain.Model.Interfaces.Services.Domain;

namespace CoffeeShop.Domain.Services.Services;

public sealed class CoffeeService : ICoffeeService
{
    private readonly ICoffeRepository _repository;

    public CoffeeService
    (
        ICoffeRepository repository
    )
    {
        _repository = repository;
    }

    public async Task CreateAsync(Coffee coffee)
    {
        await _repository.AddAsync(coffee);
    }

    public async Task DeleteAsync(Coffee coffee)
    {
        await _repository.DeleteAsync(coffee);
    }

    public async Task<List<Coffee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Coffee> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(int id, Coffee coffee)
    {
        await _repository.UpdateAsync(id, coffee);
    }
}
