using Microsoft.AspNetCore.Mvc;

namespace SignAndMarking.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}