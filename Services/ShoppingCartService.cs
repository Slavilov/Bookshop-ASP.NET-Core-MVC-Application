using Bookshop_ASP.NET_Core_MVC_Application.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SessionExtensions.Helpers;
using System.Linq;
using System.Threading.Tasks;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IBookService _bookService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;
    private const string CartSessionKey = "ShoppingCart";

    public ShoppingCartService(IBookService bookService, IHttpContextAccessor httpContextAccessor)
    {
        _bookService = bookService;
        _httpContextAccessor = httpContextAccessor;
        //_session = _httpContextAccessor.HttpContext?.Session;
    }

    public async Task AddToCart(int bookId)
    {
        var shoppingCart = GetShoppingCart();
        var book = await _bookService.GetBookByIdAsync(bookId);
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
        var cartItem = shoppingCart.Items.FirstOrDefault(i => i.BookId == bookId);
        if (cartItem != null)
        {
            shoppingCart.Items.Remove(cartItem);
        }

        SaveShoppingCart(shoppingCart);
    }

    public void ClearCart()
    {
        _session.Remove(CartSessionKey);
    }

    public List<CartItem> GetCartItems()
    {
        return GetShoppingCart().Items;
    }

    public decimal GetCartTotal()
    {
        var shoppingCart = GetShoppingCart();
        return shoppingCart.Items.Sum(i => i.Book.Price * i.Quantity);
    }
    //
    //private ShoppingCart GetShoppingCart()
    //{
    //    var session = _httpContextAccessor.HttpContext.Session;
    //    var cart = session.GetObjectFromJson<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
    //    return cart;
    //}
    //
    //private void SaveShoppingCart(ShoppingCart shoppingCart)
    //{
    //    var session = _httpContextAccessor.HttpContext.Session;
    //    session.SetObjectAsJson(CartSessionKey, shoppingCart);
    //}
    //
    private ShoppingCart GetShoppingCart()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            throw new InvalidOperationException("Session is not available.");
        }
        var cart = session.GetObjectFromJson<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();

        //Check if the session is retrieving the cart
        Console.WriteLine("Cart retrieved from session: " + JsonConvert.SerializeObject(cart));
        return cart;
    }

    private void SaveShoppingCart(ShoppingCart shoppingCart)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            throw new InvalidOperationException("Session is not available.");
        }
        session.SetObjectAsJson(CartSessionKey, shoppingCart);

        //Check if session is being set and retrieved correctly
        var sessionCart = session.GetObjectFromJson<ShoppingCart>(CartSessionKey);
        Console.WriteLine("Cart in session: " + JsonConvert.SerializeObject(sessionCart));
    }


}

