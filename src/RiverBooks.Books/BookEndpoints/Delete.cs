using FastEndpoints;

namespace RiverBooks.Books;

internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Delete("/books/{id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteBookRequest request, CancellationToken ct)
    {
        //TODO: Handle not found

        await _bookService.DeleteBookAsync(request.Id);
        await SendNoContentAsync();
    }
}
public record DeleteBookRequest(Guid Id);