using Bookshop_ASP.NET_Core_MVC_Application.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IBookService _bookService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "ShoppingCart";

    public ShoppingCartService(IBookService bookService, IHttpContextAccessor httpContextAccessor)
    {
        _bookService = bookService;
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddToCart(int bookId)
    {
        var shoppingCart = GetShoppingCart();
        var book = _bookService.GetBookByIdAsync(bookId).Result;
        if (book == null) return;

        var cartItem = shoppingCart.Items.FirstOrDefault(i => i.BookId == bookId);
        if (cartItem == null)
        {
            shoppingCart.Items.Add(new CartItem { BookId = bookId, Book = book, Quantity = 1 });
        }
        else
        {
            cartItem.Quantity++;
        }

        SaveShoppingCart(shoppingCart);
    }

    public void RemoveFromCart(int bookId)
    {
        var shoppingCart = GetShoppingCart();
        shoppingCart.Items.RemoveAll(i => i.BookId == bookId);
        SaveShoppingCart(shoppingCart);
    }

    public void ClearCart()
    {
        _httpContextAccessor.HttpContext?.Session.Remove(CartSessionKey);
    }

    public List<CartItem> GetCartItems()
    {
        return GetShoppingCart().Items;
    }

    public decimal GetCartTotal()
    {
        return GetShoppingCart().Items.Sum(i => i.Book.Price * i.Quantity);
    }

    private ShoppingCart GetShoppingCart()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        return session?.GetObjectFromJson<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
    }

    private void SaveShoppingCart(ShoppingCart shoppingCart)
    {
        _httpContextAccessor.HttpContext?.Session.SetObjectAsJson(CartSessionKey, shoppingCart);
    }
}
