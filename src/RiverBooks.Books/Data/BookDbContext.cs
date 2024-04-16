using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

public class BookDbContext : DbContext
{
    internal DbSet<Book> Books { get; set; }
    public BookDbContext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Books");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        //All our decimal properties will have a precision of 18 and a scale of 6
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }
}