using Authentication_Module.Domain.Common;
using Authentication_Module.Domain.DTO;

namespace Authentication_Module.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterDto request);

        Task<Result<LoginResponseDto>> LoginAsync(LoginDto request);

        Task<Result<TokenResponseDto>> RefreshTokenAsync(RefreshTokenDto request);
        
        Task<Result<bool>> ForgotPasswordAsync(ForgotPasswordDto request);

        Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto request);
    }
}