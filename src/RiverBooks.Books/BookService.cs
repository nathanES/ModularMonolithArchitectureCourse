namespace RiverBooks.Books;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    public async Task<List<BookDTO>> ListBooksAsync()
    {
        var books = (await _bookRepository.ListAsync())
            .Select(book => new BookDTO(book.Id, book.Title, book.Author, book.Price))
            .ToList();
        return books;
    }

    public async Task<BookDTO> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        //TODO handle not found case
        return new BookDTO(book!.Id, book.Title, book.Author, book.Price);
    }

    public async Task CreateBookAsync(BookDTO newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

    }

    public async Task DeleteBookAsync(Guid id)
    {
        var bookToDelete = _bookRepository.GetByIdAsync(id);
        if (bookToDelete is not null)
        {
            await _bookRepository.DeleteAsync(bookToDelete.Result);
            await _bookRepository.SaveChangesAsync();
        }
    }

    public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
    {
        //validate the price
        
        var book = await _bookRepository.GetByIdAsync(bookId);
        //handle not found case
        
        book!.UpdatePrice(newPrice);
        await _bookRepository.SaveChangesAsync();
    }
}