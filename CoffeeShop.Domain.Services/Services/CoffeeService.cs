using CoffeeShop.Domain.Model.Entities;
using CoffeeShop.Domain.Model.Interfaces.Repositories;
using CoffeeShop.Domain.Model.Interfaces.Services.Domain;
using CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;

namespace CoffeeShop.Domain.Services.Services;

public sealed class CoffeeService : ICoffeeService
{
    private readonly ICoffeRepository _repository;
    private readonly IBlobService _blobService;

    public CoffeeService
    (
        ICoffeRepository repository,
        IBlobService blobService
    )
    {
        _repository = repository;
        _blobService = blobService;
    }

    public async Task CreateAsync(Coffee coffee, Stream stream)
    {
        var imageUrl = await _blobService.UploadAsync(stream);

        coffee.ImageUrl = imageUrl;

        await _repository.AddAsync(coffee);
    }

    public async Task DeleteAsync(Coffee coffee)
    {
        var coffeImageUrl = coffee.ImageUrl;

        if (coffeImageUrl == null)
        {
            await _repository.DeleteAsync(coffee);
            return;
        }

        await Task.WhenAll(
            _blobService.DeleteAsync(coffeImageUrl),
            _repository.DeleteAsync(coffee));
    }

    public async Task<List<Coffee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Coffee> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(int id, Coffee coffee, Stream stream)
    {
        var actualCoffe = await _repository.GetByIdAsync(id);

        if (stream != null)
        {
            await DeleteActualCoffeImageIfAlreadyExists(actualCoffe);
            coffee.ImageUrl = await _blobService.UploadAsync(stream);
        }

        await _repository.UpdateAsync(id, coffee);
    }

    private async Task DeleteActualCoffeImageIfAlreadyExists(Coffee coffee)
    {
        var imageUrl = coffee.ImageUrl;

        if (imageUrl != null)
        {
            await _blobService.DeleteAsync(imageUrl);
        }
    }
}
