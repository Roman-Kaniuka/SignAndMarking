using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignAndMarking.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Обов'язкове поле")]
    public string Name { get; set; }

    [Required (ErrorMessage = "Обов'язкове поле")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
    public double Price { get; set; }
    
    [Required (ErrorMessage = "Обов'язкове поле")]
    public string Description { get; set; }
    
    public string Image { get; set; }

    [DisplayName("Category Type")]
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}