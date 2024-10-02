public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal GetTotalPrice()
    {
        return Items.Sum(item => item.Book.Price * item.Quantity);
    }
}
