using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationCommandService  : IAuthenticationCommandService 
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        
        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email )is not null)
            {
                throw new DuplicateEmailException();
            }

            var user = new User
            {
                FirstName = firstName,
                Email = email,
                LastName = lastName,
                Password = password
            };
            
            _userRepository.Add(user);
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
        


    }
}
