using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart.AddItem;

public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;

    public AddItemToCartHandler(IApplicationUserRepository userRepository,
        IMediator mediator, IServiceProvider serviceProvider)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Unauthorized();
        }
        // var newCartItem = new CartItem(request.BookId,"description",request.Quantity,10);
        // user.AddItemToCart(newCartItem);
        //
        // await _userRepository.SaveChangesAsync();
        // return Result.Success();
        
        var query = new BookDetailsQuery(request.BookId);
        
        var result = await _mediator.Send(query,cancellationToken);
        
         if (result.Status == ResultStatus.NotFound) 
             return Result.NotFound();
        
        var bookDetails = result.Value;
        
        string description = $"{bookDetails.Title} by {bookDetails.Author}";
        
        var newCartItem = new CartItem(request.BookId,
            description,
            request.Quantity,
            bookDetails.Price);
        
        user.AddItemToCart(newCartItem);
        
        await _userRepository.SaveChangesAsync();
        return Result.Success();
    }
}