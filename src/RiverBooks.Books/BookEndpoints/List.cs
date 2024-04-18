using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

//FastEndpoints
internal class List(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{
    //Using REPR design pattern.
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var books = await _bookService.ListBooksAsync();
        
        await SendAsync(new ListBooksResponse()
        {
            Books = books
        });
    }
}
public class ListBooksResponse
{
    public List<BookDto> Books { get; set; } = new List<BookDto>();
}