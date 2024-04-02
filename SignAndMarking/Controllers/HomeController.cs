using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignAndMarking.Data;
using SignAndMarking.Models;
using SignAndMarking.Models.ViewModels;

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

    public IActionResult Details(int? Id)
    {
        DetailsVM detailsVM = new DetailsVM
        {
            Product = _db.Products
                .Include(u=>u.Category)
                .Include(u=>u.Feature)
                .Where(u=>u.Id==Id)
                .FirstOrDefault(),
            ExistsInBasket = false,
        };
        return View(detailsVM);
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