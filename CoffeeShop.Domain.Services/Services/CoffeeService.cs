using CoffeeShop.Domain.Model.DTOs;
using CoffeeShop.Domain.Model.Entities;
using CoffeeShop.Domain.Model.Interfaces.Repositories;
using CoffeeShop.Domain.Model.Interfaces.Services.Domain;
using CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;
using CoffeShop.Domain.Model.Interfaces.Services.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace CoffeeShop.Domain.Services.Services;

public sealed class CoffeeService : ICoffeeService
{
    private readonly ICoffeRepository _repository;
    private readonly IBlobService _blobService;
    private readonly IQueueService _queueService;

    public CoffeeService
    (
        ICoffeRepository repository,
        IBlobService blobService,
        IQueueService queueService
    )
    {
        _repository = repository;
        _blobService = blobService;
        _queueService = queueService;
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

        await Task.WhenAll(
            _blobService.DeleteAsync(coffeImageUrl),
            _repository.DeleteAsync(coffee));
    }

    public async Task<List<Coffee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Coffee> GetByIdAsync(int id, bool SendVisualizationMessage)
    {
        var coffe = await _repository.GetByIdAsync(id);

        if (coffe != null && SendVisualizationMessage)
            await SendVisualizationMessageToQueue(coffe);

        return coffe;
    }

    public async Task UpdateAsync(int id, CoffeeDTO coffeeDTO, Stream stream)
    {
        var actualDbCoffee = await _repository.GetByIdAsync(id);
        var actualImageUrl = actualDbCoffee.ImageUrl;

        if (stream != null)
        {
            await _blobService.DeleteAsync(actualImageUrl);

            coffeeDTO.ImageUrl = await _blobService.UploadAsync(stream);

            await _repository.UpdateAsync(id, coffeeDTO);
            return;
        }

        coffeeDTO.ImageUrl = actualImageUrl;
        await _repository.UpdateAsync(id, coffeeDTO);
    }

    private async Task SendVisualizationMessageToQueue(Coffee coffee)
    {
        string formatedCoffe = FormatCoffeeToBase64(coffee);

        await _queueService.SendAsync(formatedCoffe);
    }

    private static string FormatCoffeeToBase64(Coffee coffee)
    {
        var jsonCoffee = JsonConvert.SerializeObject(coffee);

        var bytesJsonCoffee = UTF8Encoding.UTF8.GetBytes(jsonCoffee);

        return Convert.ToBase64String(bytesJsonCoffee);
    }
}
