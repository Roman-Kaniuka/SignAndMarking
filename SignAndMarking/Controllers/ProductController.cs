using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        IEnumerable<Product> products = _db.Products
            .Include(u=>u.Category)
            .Include(u=>u.Feature);
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
            }),
            FeatureSelectList = _db.Features.Select(u=> new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
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

    public IActionResult Edit(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        var product = _db.Products.Find(Id);
        if (product == null)
        {
            return NotFound();
        }
        ProductVM productVM = new ProductVM
        {
            Product = product,
            CategorySelectList = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            FeatureSelectList = _db.Features.Select(u=>new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
        };
        return View(productVM);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductVM productVM)
    {
        var files = HttpContext.Request.Form.Files;
        string webRootPath = _webHostEnvironment.WebRootPath;
        var oldProductFromDB = _db.Products.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);
        
        if (files.Count > 0)
        {
            string upload = webRootPath + WC.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            
            var oldFilePath = upload + oldProductFromDB.Image;
            var oldFile = new FileInfo(oldFilePath);
            
            if (oldFile.Exists)
            {
                oldFile.Delete();
            }
            using ( var fileStream = new FileStream(Path.Combine(upload, fileName+extension),FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            productVM.Product.Image = fileName + extension;
        }
        else
        {
            productVM.Product.Image = oldProductFromDB.Image;
        }
        
        _db.Products.Update(productVM.Product);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    //GET
    public IActionResult Delete(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }
        var product = _db.Products
            .Include(u=>u.Category)
            .Include(u=>u.Feature)
            .FirstOrDefault(u=>u.Id==Id);
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? Id)
    {
        var product = _db.Products.Find(Id);
        if (product==null)
        {
            return NotFound();
        }
        var upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
        var oldFilePath = Path.Combine(upload, product.Image);
        var oldFile = new FileInfo(oldFilePath);
        if (oldFile.Exists)
        {
            oldFile.Delete();
        }
        _db.Products.Remove(product);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}