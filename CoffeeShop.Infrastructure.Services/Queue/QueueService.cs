using Azure.Storage.Queues;
using CoffeShop.Domain.Model.Interfaces.Services.Infrastructure;

namespace CoffeeShop.Infrastructure.Services.Queue;

public sealed class QueueService : IQueueService
{
    private readonly QueueServiceClient _queueServiceClient;
    private const string _queueName = "func-update-last-time-visualization";

    public QueueService
    (
        string storageAccount
    )
    {
        _queueServiceClient = new QueueServiceClient(storageAccount);
    }

    public async Task SendAsync(string message)
    {
        var queueClient = _queueServiceClient.GetQueueClient(_queueName);

        await queueClient.CreateIfNotExistsAsync();

        await queueClient.SendMessageAsync(message);
    }
}
