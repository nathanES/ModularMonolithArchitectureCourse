using FastEndpoints.Testing;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture, ITestOutputHelper outputHelper) :
    TestClass<Fixture>(fixture, outputHelper)
{
    
}