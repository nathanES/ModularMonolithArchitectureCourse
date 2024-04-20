using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMediator _mediator;

    public AddItemToCartHandler(IApplicationUserRepository userRepository,
        IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }
        var newCartItem = new CartItem(request.BookId,"description",request.Quantity,10);
        user.AddItemToCart(newCartItem);

        await _userRepository.SaveChangesAsync();
        return Result.Success();
        // var query = new BookDetailsQuery(request.BookId);
        //
        // var result = await _mediator.Send(query);
        //
        // if (result.Status == ResultStatus.NotFound) 
        //     return Result.NotFound();
        //
        // var bookDetails = result.Value;
        //
        // string description = $"{bookDetails.Title} by {bookDetails.Author}";
        //
        // var newCartItem = new CartItem(request.BookId,
        //     description,
        //     request.Quantity,
        //     bookDetails.Price);
        //
        // user.AddItemToCart(newCartItem);
        //
        // await _userRepository.SaveChangesAsync();
        // return Result.Success();
    }
}
public record BookDetailsQuery(Guid BookId) : IRequest<Result<BookDetailsResponse>>;
public record BookDetailsResponse(Guid BookId, string Title, 
    string Author, decimal Price);