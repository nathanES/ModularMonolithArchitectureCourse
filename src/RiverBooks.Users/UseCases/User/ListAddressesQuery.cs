using Ardalis.Result;
using MediatR;
using RiverBooks.Users.UserEnpoints;

namespace RiverBooks.Users.UseCases.User;

internal record ListAddressesQuery(string EmailAddress) :
    IRequest<Result<List<UserAddressDto>>>;