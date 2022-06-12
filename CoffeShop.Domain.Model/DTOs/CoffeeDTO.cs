namespace CoffeeShop.Domain.Model.DTOs;

public sealed class CoffeeDTO
{
    public string BrandName { get; set; }
    public string ProductorName { get; set; }
    public int Altitude { get; set; }
    public string Location { get; set; }
    public string ImageUrl { get; set; }
}
