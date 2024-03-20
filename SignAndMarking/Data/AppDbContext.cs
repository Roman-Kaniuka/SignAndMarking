using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;

namespace SignAndMarking.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }
    
}