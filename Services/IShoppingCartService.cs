public interface IShoppingCartService
{
    Task AddToCart(int bookId);
    void RemoveFromCart(int bookId);
    void ClearCart();
    List<CartItem> GetCartItems();
    decimal GetCartTotal();
}
