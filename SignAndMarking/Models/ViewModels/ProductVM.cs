using Microsoft.AspNetCore.Mvc.Rendering;

namespace SignAndMarking.Models.ViewModels;

public class ProductVM
{
    public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    public IEnumerable<SelectListItem> FeatureSelectList { get; set; }
    public Product Product { get; set; }
    
}