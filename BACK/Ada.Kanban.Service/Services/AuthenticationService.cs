using Ada.Kanban.Common.Exceptions;
using Ada.Kanban.Service.Models;
using Ada.Kanban.Service.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ada.Kanban.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly LoginCredentialOptions _loginCredentialOptions;

        public AuthenticationService(IOptions<JwtOptions> jwtOptions, IOptions<LoginCredentialOptions> loginCredentialOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _loginCredentialOptions = loginCredentialOptions.Value;
        }

        public string Authenticate(LoginCredentialModel loginCredential)
        {
            if (loginCredential.Login != _loginCredentialOptions.Login || loginCredential.Senha != _loginCredentialOptions.Senha)
            {
                throw new AdaKanbanException(AdaKanbanExceptionType.Unauthorized, "Unauthorized");
            }

            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Key!);
            var expirationInMinutes = _jwtOptions.ExpirationInMinutes;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, loginCredential.Login!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
