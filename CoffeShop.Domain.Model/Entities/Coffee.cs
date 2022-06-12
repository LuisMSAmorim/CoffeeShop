using CoffeeShop.Domain.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Domain.Model.Entities;

public sealed class Coffee : BaseEntity
{
    [Required]
    [Display(Name = "Marca")]
    public string BrandName { get; set; }
    [Required]
    [Display(Name = "Nome do Produtor")]
    public string ProductorName { get; set; }
    [Required]
    [Display(Name = "Altitude de Plantio")]
    public int Altitude { get; set; }
    [Required]
    [Display(Name = "Localização de Plantio")]
    public string Location { get; set; }
    public string ImageUrl { get; set; }
}
