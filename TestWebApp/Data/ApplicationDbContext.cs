using Microsoft.EntityFrameworkCore;
using TestRestApplication.Models;

namespace TestRestApplication.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Info> Infos { get; set; }
}