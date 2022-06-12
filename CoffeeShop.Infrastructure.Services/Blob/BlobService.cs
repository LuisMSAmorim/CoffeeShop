using Azure.Storage.Blobs;
using CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;

namespace CoffeeShop.Infrastructure.Services.Blob;

public sealed class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    private const string _container = "coffeescontainer";

    public BlobService
    (
        string storageAccount
    )
    {
        _blobServiceClient = new BlobServiceClient(storageAccount);
    }

    public async Task<string> UploadAsync(Stream stream)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

        await CreateContainerIfNotExists(containerClient);

        var blobClient = containerClient.GetBlobClient($"coffee{Guid.NewGuid()}.jpg");

        bool overwrite = true;

        await blobClient.UploadAsync(stream, overwrite);

        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string blobName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

        Uri uri = new(blobName);
        BlobClient blob = new(uri);

        var blobClient = containerClient.GetBlobClient(blob.Name);

        await blobClient.DeleteIfExistsAsync();
    }

    private static async Task CreateContainerIfNotExists(BlobContainerClient containerClient)
    {
        if (await containerClient.ExistsAsync())
            return;

        await containerClient.CreateIfNotExistsAsync();
        await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

    }
}
