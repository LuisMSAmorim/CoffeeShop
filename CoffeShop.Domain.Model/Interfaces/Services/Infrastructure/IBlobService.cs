namespace CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;

public interface IBlobService
{
    Task<string> UploadAsync(Stream stream);
    Task DeleteAsync(string blobName);
}
