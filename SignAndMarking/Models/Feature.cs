using System.ComponentModel.DataAnnotations;

namespace SignAndMarking.Models;

public class Feature
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Обов'язкове поле")]
    public string Name { get; set; }
}