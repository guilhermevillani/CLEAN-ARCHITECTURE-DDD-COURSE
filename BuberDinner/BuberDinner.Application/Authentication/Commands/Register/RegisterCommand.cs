using BuberDinner.Application.Services.Authentication;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string firstName,
    string lastName,
    string email,
    string password):IRequest<AuthenticationResult>;
