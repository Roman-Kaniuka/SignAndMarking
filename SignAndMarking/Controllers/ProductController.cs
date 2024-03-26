using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignAndMarking.Data;
using SignAndMarking.Models;

namespace SignAndMarking.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _db;
    public ProductController(AppDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Product> products = _db.Products;
        foreach (var product in products)
        {
            product.Category = _db.Categories.FirstOrDefault((u => u.Id == product.CategoryId));
        }
        return View(products);
    }

    //GET
    public IActionResult Create()
    {
        IEnumerable <SelectListItem> categoryDropDown = _db.Categories.Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Id.ToString(),
        });
        ViewBag.CategoryDropDown = categoryDropDown;
        return View();
    }
}