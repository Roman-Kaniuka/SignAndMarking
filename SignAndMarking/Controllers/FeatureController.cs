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
    //GET
    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        var feature = _db.Features.Find(Id);
        if (feature == null)
        {
            return NotFound();
        }
        return View(feature);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Feature feature)
    {
        if (ModelState.IsValid)
        {
            _db.Features.Update(feature);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(feature);
    }
    //GET
    public IActionResult Delete(int Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        var feature = _db.Features.Find(Id);

        if (feature == null)
        {
            return NotFound();
        }

        return View(feature);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        var feature = _db.Features.Find(Id);

        if (feature == null)
        {
            return NotFound();
        }

        _db.Features.Remove(feature);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}