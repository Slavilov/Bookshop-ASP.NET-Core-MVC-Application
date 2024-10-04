using Microsoft.AspNetCore.Mvc;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public IActionResult Index()
    {
        var items = _shoppingCartService.GetCartItems();
        ViewBag.TotalPrice = _shoppingCartService.GetCartTotal();
        return View(items);
    }

    public IActionResult AddToCart(int bookId)
    {
        _shoppingCartService.AddToCart(bookId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult RemoveFromCart(int bookId)
    {
        _shoppingCartService.RemoveFromCart(bookId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ClearCart()
    {
        _shoppingCartService.ClearCart();
        return RedirectToAction(nameof(Index));
    }
}
