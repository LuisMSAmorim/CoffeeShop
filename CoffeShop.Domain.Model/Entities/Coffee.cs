using CoffeeShop.Domain.Model.Entities.Common;

namespace CoffeeShop.Domain.Model.Entities;

public sealed class Coffee : BaseEntity
{
    public string BrandName { get; set; }
    public string ProductorName { get; set; }
    public int Altitude { get; set; }
    public string Location { get; set; }
    public string ImageUrl { get; set; }
}
