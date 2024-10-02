using Microsoft.AspNetCore.Mvc;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }


    public IActionResult CheckSession()
    {
        var testValue = HttpContext.Session.GetString("Test");
        if (string.IsNullOrEmpty(testValue))
        {
            HttpContext.Session.SetString("Test", "Session is working!");
        }
        return Content(HttpContext.Session.GetString("Test"));
    }

    public IActionResult Index()
    {
        var items = _shoppingCartService.GetCartItems();
        var total = _shoppingCartService.GetCartTotal();

        ViewBag.Items = items;
        ViewBag.TotalPrice = total;

        return View();
    }

    public IActionResult AddToCart(int bookId)
    {
        _shoppingCartService.AddToCart(bookId);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int bookId)
    {
        _shoppingCartService.RemoveFromCart(bookId);
        return RedirectToAction("Index");
    }

    public IActionResult ClearCart()
    {
        _shoppingCartService.ClearCart();
        return RedirectToAction("Index");
    }
}
