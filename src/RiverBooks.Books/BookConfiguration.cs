using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

//Is a file who contains the configuration of the Book entity in our database
internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    private Guid Book1Guid = new Guid("86641840-9e9c-432e-a36b-97a3e28e8c93");
    private Guid Book2Guid = new Guid("166ae563-6732-445d-b0bb-621f152b9c42");
    private Guid Book3Guid = new Guid("21cb3bb0-0313-4343-bd4b-a330104934e5");

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(p => p.Author)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();
        builder.HasData(GetSampleBookData());
    }

    private IEnumerable<Book> GetSampleBookData()
    {
        var tolkien = "J.R.R. Tolkien";
        yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
        yield return new Book(Book2Guid, "The Two Towers", tolkien, 11.99m);
        yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
    }
}