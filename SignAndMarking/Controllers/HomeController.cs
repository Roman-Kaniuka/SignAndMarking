using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignAndMarking.Data;
using SignAndMarking.Models;
using SignAndMarking.Models.ViewModels;
using SignAndMarking.Utility;

namespace SignAndMarking.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    //GET
    public IActionResult Index()
    {
        HomeVM homeVM = new HomeVM
        {
            Products = _db.Products
                .Include(u=>u.Category)
                .Include(u=>u.Feature),
            Categories = _db.Categories,
        };
        
        return View(homeVM);
    }
    //GET
    public IActionResult Details(int? Id)
    {
        List<ShoppingBasket> shoppingBasketList = new List<ShoppingBasket>();
        if(HttpContext.Session.Get<IEnumerable<ShoppingBasket>>(WC.SessionBasket) != null
           && HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket).Count() > 0)
        {
            shoppingBasketList = HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket);
        }
        
        DetailsVM detailsVM = new DetailsVM
        {
            Product = _db.Products
                .Include(u=>u.Category)
                .Include(u=>u.Feature)
                .Where(u=>u.Id==Id)
                .FirstOrDefault(),
            ExistsInBasket = false,
        };
        foreach (var item in shoppingBasketList)
        {
            if (item.ProductId == Id)
            {
                detailsVM.ExistsInBasket = true;
            }
        }
        return View(detailsVM);
    }
    //POST
    [HttpPost, ActionName("Details")]
    public IActionResult DetailsPost(int id)
    {
        List<ShoppingBasket> shoppingBasketList = new List<ShoppingBasket>();
        if(HttpContext.Session.Get<IEnumerable<ShoppingBasket>>(WC.SessionBasket) != null
           && HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket).Count() > 0)
        {
            shoppingBasketList = HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket);
        }
        shoppingBasketList.Add(new ShoppingBasket { ProductId = id });
        HttpContext.Session.Set(WC.SessionBasket, shoppingBasketList);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromBasket(int Id)
    {
        List<ShoppingBasket> shoppingBasketList = new List<ShoppingBasket>();
        if(HttpContext.Session.Get<IEnumerable<ShoppingBasket>>(WC.SessionBasket) != null
           && HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket).Count() > 0)
        {
            shoppingBasketList = HttpContext.Session.Get<List<ShoppingBasket>>(WC.SessionBasket);
        }

        var itemToRemove = shoppingBasketList.SingleOrDefault(u => u.ProductId == Id);
        if (itemToRemove != null)
        {
            shoppingBasketList.Remove(itemToRemove);
        }
        HttpContext.Session.Set(WC.SessionBasket, shoppingBasketList);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}