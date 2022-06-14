namespace CoffeShop.Domain.Model.Interfaces.Services.Infrastructure;

public interface IQueueService{
    Task SendAsync(string messageText);
}