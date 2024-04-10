namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public List<BookDTO> ListBooks()
    {
        return
        [
            new BookDTO(Guid.NewGuid(), "The Fellowship of the Ring", "JRR Tolkien"),
            new BookDTO(Guid.NewGuid(), "The Two Towers", "JRR Tolkien"),
            new BookDTO(Guid.NewGuid(), "The Return of the King", "JRR Tolkien")
        ];
    }
}