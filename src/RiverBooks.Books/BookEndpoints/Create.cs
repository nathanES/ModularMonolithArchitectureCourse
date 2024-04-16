using FastEndpoints;

namespace RiverBooks.Books;

internal class Create(IBookService bookService) : Endpoint<CreateBookRequest, BookDTO>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CreateBookRequest request, CancellationToken ct)
    {
        var newBookDTO = new BookDTO(request.Id ?? Guid.NewGuid(),
            request.Title,
            request.Author,
            request.Price);
        
        await _bookService.CreateBookAsync(newBookDTO);
        await SendCreatedAtAsync<GetById>(new { newBookDTO.Id }, newBookDTO);
    }
}
//If you can't nasted this class you can add it to the endpoint file
public class CreateBookRequest
{
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; }= string.Empty;
    public decimal Price { get; set; }
}