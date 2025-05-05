namespace Basket.API.Entities;

public class Cart
{
    public string UserName { get; set; }
    public List<CartItem> Items { get; set; }

    public Cart()
    {
    }

    public Cart(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice => Items.Sum(x => x.ProductPrice * x.Quantity);
}