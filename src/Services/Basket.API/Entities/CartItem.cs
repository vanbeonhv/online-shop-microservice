using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities;

public class CartItem
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0.")]
    public int Quantity { get; set; }
    [Required]
    [Range(0.1, double.PositiveInfinity, ErrorMessage = "Price must be greater than or equal to 1.")]
    public decimal ProductPrice { get; set; }
    public string ItemNo { get; set; }
    public string ItemName { get; set; }
}
