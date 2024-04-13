using System.Reflection;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

public class BookDbContext : DbContext
{
    internal DbSet<Book> Books { get; set; }
const string Schema = "Books";
    public BookDbContext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
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
internal class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Book(Guid id, string title, string author, decimal price)
    {
        Id = Guard.Against.Default(id);
        Title = Guard.Against.NullOrEmpty(title);
        Author = Guard.Against.NullOrEmpty(author);
        Price = Guard.Against.Negative(price);
    }

    internal void UpdatePrice(decimal newPrice)
    {
        Price = Guard.Against.Negative(newPrice);
    }
}