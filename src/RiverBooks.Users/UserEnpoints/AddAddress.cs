using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User;
using RiverBooks.Users.UseCases.User.AddAddress;

namespace RiverBooks.Users.UserEnpoints;

internal sealed class AddAddress : Endpoint<AddAddressRequest>
{
    private readonly IMediator _mediator;

    public AddAddress(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddAddressRequest request,
        CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddAddressToUserCommand(emailAddress!,
            request.Street1,
            request.Street2,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);

        var result = await _mediator.Send(command);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await SendUnauthorizedAsync();
        }
        else
        {
            await SendOkAsync();
        }
    }
}
internal record AddAddressRequest(string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);