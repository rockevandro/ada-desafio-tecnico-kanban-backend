using Ada.Kanban.Service.Models;

namespace Ada.Kanban.Service.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(LoginCredentialModel loginCredential);
    }
}