using FastEndpoints;

namespace RiverBooks.Books;

internal class GetById(IBookService bookService) :
    Endpoint<GetBookByIdRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var book = await _bookService.GetBookByIdAsync(req.Id);
        if (book is null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(book);
    }
}

public class GetBookByIdRequest
{
    public Guid Id { get; set; }
}