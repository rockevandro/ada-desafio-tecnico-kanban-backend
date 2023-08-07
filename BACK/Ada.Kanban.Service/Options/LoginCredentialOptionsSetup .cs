using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ada.Kanban.Service.Options
{
    public class LoginCredentialOptionsSetup : IConfigureOptions<LoginCredentialOptions>
    {
        private const string SectionName = "LoginCredentials";
        private readonly IConfiguration _configuration;

        public LoginCredentialOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(LoginCredentialOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
