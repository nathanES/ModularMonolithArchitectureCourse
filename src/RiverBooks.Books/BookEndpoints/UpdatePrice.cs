using FastEndpoints;

namespace RiverBooks.Books;

internal class UpdatePrice (IBookService bookService):
    Endpoint<UpdateBookPriceRequest, BookDTO>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        //Normally when updating you should update all fields not just the price.
        Post("/books/{id}/pricehistory");
        AllowAnonymous();
    }
    public override async Task HandleAsync(UpdateBookPriceRequest request, CancellationToken ct)
    {
        //TODO Handle not found
        await _bookService.UpdateBookPriceAsync(request.Id, request.NewPrice);
        
        var updatedBook = await _bookService.GetBookByIdAsync(request.Id);
        await SendAsync(updatedBook);
    }
}

public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);