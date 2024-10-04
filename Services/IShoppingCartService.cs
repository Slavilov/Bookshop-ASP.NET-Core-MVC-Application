public interface IShoppingCartService
{
    void AddToCart(int bookId);
    void RemoveFromCart(int bookId);
    void ClearCart();
    List<CartItem> GetCartItems();
    decimal GetCartTotal();
}
