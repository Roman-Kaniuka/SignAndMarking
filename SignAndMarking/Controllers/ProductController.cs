using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignAndMarking.Data;
using SignAndMarking.Models;
using SignAndMarking.Models.ViewModels;

namespace SignAndMarking.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
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
        ProductVM productVM = new ProductVM
        {
            CategorySelectList = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };
        return View(productVM);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductVM productVM)
    {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + WC.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);
            using ( var fileStream = new FileStream(Path.Combine(upload, fileName+extension),FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            productVM.Product.Image = fileName + extension;
            _db.Products.Add(productVM.Product);
            _db.SaveChanges();
            return RedirectToAction("Index");
    }
    
}