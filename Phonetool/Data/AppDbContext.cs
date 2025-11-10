using Microsoft.EntityFrameworkCore;
using Phonetool.Entities;

namespace Phonetool.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Person> Persons { get; set; }
}
