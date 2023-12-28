using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagment.Api.Controllers
{
    [Route ("Api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login (AuthRequest request)
        {
            return Ok(await _authService.Login (request));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register (RegistrationRequest request)
        {
            return Ok(await _authService.Register (request));
        }
    }
}
