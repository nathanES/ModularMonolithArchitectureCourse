using FastEndpoints;
using FastEndpoints.Testing;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RiverBooks.Tests.Endpoints;

public class BookList(Fixture fixture, ITestOutputHelper outputHelper):
    TestClass<Fixture>(fixture, outputHelper)
{
    [Fact]
    public async Task ReturnsThreeBooksAsync()
    {
        var testRestult = await Fixture.Client.GETAsync<List, ListBookResponse>();
        testRestult.Result.EnsureSuccessStatusCode();
        testRestult.Result.Books.Count.Should().Be(3);
    }
}