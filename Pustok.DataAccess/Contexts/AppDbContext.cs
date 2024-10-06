using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities;

namespace Pustok.DataAccess.Contexts;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   
    {
        
    }

    public DbSet<Product> Products { get; set; } = null!;
}
