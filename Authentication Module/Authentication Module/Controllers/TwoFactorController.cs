using Authentication_Module.Domain.DTO;
using Authentication_Module.Domain.Interfaces.Services;
using Authentication_Module.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authentication_Module.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TwoFactorController : ControllerBase
    {
        private readonly ITwoFactorService _2faService;
        private readonly ApplicationDbContext _context;

        public TwoFactorController(ITwoFactorService tfaService, ApplicationDbContext context)
        {
            _2faService = tfaService;
            _context = context;
        }

        [HttpPost("setup")]
        public async Task<IActionResult> Setup()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var secret = _2faService.GenerateSecret();
            user.TwoFactorSecret = secret;
            await _context.SaveChangesAsync();

            var qrCode = _2faService.GetQrCodeBase64(user.Email, secret);

            return Ok(new { QrCode = qrCode, ManualKey = secret });
        }

        [HttpPost("verify-and-enable")]
        public async Task<IActionResult> Verify([FromBody] Verify2FADto request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!_2faService.ValidateCode(user.TwoFactorSecret!, request.Code))
            {
                return BadRequest("Invalid verification code.");
            }

            user.TwoFactorEnabled = true;
            await _context.SaveChangesAsync();

            return Ok("Two-factor authentication enabled successfully.");
        }
    }
}
