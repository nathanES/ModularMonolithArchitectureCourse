using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;

namespace RiverBooks.Users.CartEndpoints;

public class AddItem : Endpoint<AddCartItemRequest>
{
    private readonly IMediator _mediator;

    public AddItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest request,
        CancellationToken cancellationToken)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddItemToCartCommand(request.BookId, request.Quantity, emailAddress!);

        var result = await _mediator!.Send(command, cancellationToken);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
        }
        else if (result.Status == ResultStatus.Invalid)
        {
            await SendResultAsync(result.ToMinimalApiResult());
        }
        else
        {
            await SendOkAsync();
        }
    }
}
public record AddCartItemRequest(Guid BookId, int Quantity);