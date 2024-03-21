using System.ComponentModel.DataAnnotations;

namespace SignAndMarking.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Обов'язкове поле")]
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
}