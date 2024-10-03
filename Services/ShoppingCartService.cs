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
    private const string CartSessionKey = "ShoppingCart";

    public ShoppingCartService(IBookService bookService, IHttpContextAccessor httpContextAccessor)
    {
        _bookService = bookService;
        _httpContextAccessor = httpContextAccessor;
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
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            throw new InvalidOperationException("Session is not available.");
        }
        session.Remove(CartSessionKey);
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

        // Serialize and store the cart
        session.SetObjectAsJson(CartSessionKey, shoppingCart);

        // DEBUG: Retrieve the cart immediately after saving to confirm it was saved
        var sessionCart = session.GetObjectFromJson<ShoppingCart>(CartSessionKey);

        // Log the cart to the console
        Console.WriteLine("Cart in session after Save: " + JsonConvert.SerializeObject(sessionCart));

        if (sessionCart == null || sessionCart.Items.Count == 0)
        {
            Console.WriteLine("Error: Shopping cart was not saved properly.");
        }
    }
}


