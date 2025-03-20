using BuberDinner.Application.Services.Authentication;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries;

public record LoginQuery(
    string email,
    string password) : IRequest<AuthenticationResult>;
