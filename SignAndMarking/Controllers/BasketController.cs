using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignAndMarking.Data;
using SignAndMarking.Models;
using SignAndMarking.Utility;

namespace SignAndMarking.Controllers;

public class BasketController : Controller
{
    private readonly AppDbContext _db;

    public BasketController(AppDbContext db)
    {
        _db = db;

    }
    // GET
    public IActionResult Index()
    {
        List<ShoppingBasket> shoppingBasketsList = new List<ShoppingBasket>();
        if (HttpContext.Session.Get<IEnumerable<ShoppingBasket>>(WC.SessionBasket) != null &&
            HttpContext.Session.Get<IEnumerable<ShoppingBasket>>(WC.SessionBasket).Count() > 0)
        {
            shoppingBasketsList = HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket);
        }
        List<int> productIdBasket = shoppingBasketsList.Select(i => i.ProductId).ToList();
        IEnumerable<Product> productsList = _db.Products.Where(i => productIdBasket.Contains(i.Id));
        return View(productsList);
    }

    public IActionResult Remove(int? Id)
    {
        List<ShoppingBasket> shoppingBasketList = HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket);
        shoppingBasketList.Remove(shoppingBasketList.FirstOrDefault(i => i.ProductId == Id));
        HttpContext.Session.Set(WC.SessionBasket, shoppingBasketList);
        
        return RedirectToAction("Index");
    }
}