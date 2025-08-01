namespace Shared.DTOs.Basket;

public class CartDto
{
    public string UserName { get; set; }
    public List<CartItemDto> Items { get; set; }
}