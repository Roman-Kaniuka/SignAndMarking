using Microsoft.AspNetCore.Mvc;
using SignAndMarking.Data;
using SignAndMarking.Models;

namespace SignAndMarking.Controllers;

public class CategoryController : Controller
{
    private readonly AppDbContext _db;
    public CategoryController(AppDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> categoriesList= _db.Categories;
        return View(categoriesList);
    }
    // GET
    public IActionResult Create()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(category);
    }
    // GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var category = _db.Categories.Find(id);
        
        if(category == null)
        {
            return NotFound();
        }

        return View(category);
    }
}