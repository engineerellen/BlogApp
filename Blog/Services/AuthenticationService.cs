using BlogApp.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly BlogContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(BlogContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username && u.PasswordHash == password);

            if (user == null)
            {
                return null;
            }

            //criação do token atraves do jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserId.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}