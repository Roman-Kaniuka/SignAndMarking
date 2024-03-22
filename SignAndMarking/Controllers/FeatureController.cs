using Microsoft.AspNetCore.Mvc;
using SignAndMarking.Data;
using SignAndMarking.Models;

namespace SignAndMarking.Controllers;

public class FeatureController : Controller
{
    private readonly AppDbContext _db;
    public FeatureController(AppDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Feature> featuresList = _db.Features;
        return View(featuresList);
    }
    
    //GET
    public IActionResult Create()
    {
        return View();
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Feature feature)
    {
        if (ModelState.IsValid)
        {
            _db.Features.Add(feature);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(feature);
    }
}