using System.ComponentModel.DataAnnotations;

namespace SignAndMarking.Models;

public class Feature
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}