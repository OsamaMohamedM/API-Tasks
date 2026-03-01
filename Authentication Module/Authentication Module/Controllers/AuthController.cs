using Authentication_Module.Domain.DTO;
using Authentication_Module.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Module.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Error = result.ErrorMessage });
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { Error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { Error = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
    }
}