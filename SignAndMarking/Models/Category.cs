using System.ComponentModel.DataAnnotations;

namespace SignAndMarking.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Обов'язкове поле")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Обов'язкове поле")]
    [Range(1, int.MaxValue, ErrorMessage = "Значення має бути більше 0")]
    public int DisplayOrder { get; set; }
}