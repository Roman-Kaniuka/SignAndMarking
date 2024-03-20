using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using SignAndMarking.Models;

namespace SignAndMarking.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    
}