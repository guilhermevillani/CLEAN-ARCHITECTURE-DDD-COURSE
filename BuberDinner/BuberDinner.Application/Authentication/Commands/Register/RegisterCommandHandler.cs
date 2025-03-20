using MediatR;
using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>    
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.email )is not null)
        {
            throw new DuplicateEmailException();
        }

        var user = new User
        {
            FirstName = command.firstName,
            Email = command.email,
            LastName = command.lastName,
            Password = command.password
        };
            
        _userRepository.Add(user);
            
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user,token);    
    }
}