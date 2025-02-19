using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email )is not null)
            {
                throw new Exception("Email address already exists");
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
        public AuthenticationResult Login(string email, string password)
        {
            //var userId = Guid.NewGuid();
            //var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("Invalid informations");
            }

            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            
            var token = _jwtTokenGenerator.GenerateToken(user);
            
            return new AuthenticationResult(user, token);
        }


    }
}
