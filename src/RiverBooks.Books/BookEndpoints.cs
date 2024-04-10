using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.ListBooks();
        });
    }
}

internal interface IBookService
{
    IEnumerable<BookDTO> ListBooks();
}

public record BookDTO(Guid Id, string Title, string Author);

internal class BookService : IBookService
{
    public IEnumerable<BookDTO> ListBooks()
    {
        return
        [
            new BookDTO(Guid.NewGuid(), "The Fellowship of the Ring", "JRR Tolkien"),
            new BookDTO(Guid.NewGuid(), "The Two Towers", "JRR Tolkien"),
            new BookDTO(Guid.NewGuid(), "The Return of the King", "JRR Tolkien")
        ];
    }
}

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}

