using Ada.Kanban.Service.Models;
using Ada.Kanban.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ada.Kanban.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(LoginCredentialModel loginCredential)
        {
            var token = _authenticationService.Authenticate(loginCredential);
            return Ok(token);
        }
    }
}