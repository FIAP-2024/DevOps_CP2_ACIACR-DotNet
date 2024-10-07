using CleanOceanic.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanOceanic.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Volunteer> Volunteers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
