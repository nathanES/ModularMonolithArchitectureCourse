using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookList(Fixture fixture, ITestOutputHelper outputHelper):
    TestClass<Fixture>(fixture, outputHelper)
{
    [Fact]
    public async Task ReturnsThreeBooksAsync()
    {
        var testRestult = await Fixture.Client.GETAsync<List, ListBooksResponse>();
        testRestult.Response.EnsureSuccessStatusCode();
        testRestult.Result.Books.Count.Should().Be(3);
    }
}