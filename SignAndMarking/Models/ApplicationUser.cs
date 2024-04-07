using Microsoft.AspNetCore.Identity;

namespace SignAndMarking.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}