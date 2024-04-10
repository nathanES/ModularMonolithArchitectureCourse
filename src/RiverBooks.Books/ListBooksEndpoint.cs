using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

//FastEndpoints
internal class ListBooksEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
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
        var books = _bookService.ListBooks();
        
        await SendAsync(new ListBooksResponse()
        {
            Books = _bookService.ListBooks()
        });
    }
    
}