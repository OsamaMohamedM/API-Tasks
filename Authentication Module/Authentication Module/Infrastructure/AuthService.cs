using Authentication_Module.Domain.Common;
using Authentication_Module.Domain.DTO;
using Authentication_Module.Domain.Entities;
using Authentication_Module.Domain.Interfaces.Repos;
using Authentication_Module.Domain.Interfaces.Services;

namespace Authentication_Module.Infrastructure
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository, ITokenService tokenProvider, IRefreshTokenRepository refreshTokenRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
        }

        public async Task<Result> RegisterAsync(RegisterDto request)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(request.UserName);

            if (existingUser != null)
            {
                return Result.Failure("UserName already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.UserName,
                PasswordHash = passwordHash,
                Email = request.Email
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<TokenResponseDto>> LoginAsync(LoginDto request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Result<TokenResponseDto>.Failure("Invalid UserName or password.");
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return Result<TokenResponseDto>.Success(new TokenResponseDto(accessToken, refreshToken));
        }

        public async Task<Result<TokenResponseDto>> RefreshTokenAsync(RefreshTokenDto request)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result<TokenResponseDto>.Failure("Invalid or expired refresh token.");
            }

            refreshToken.IsUsed = true;

            var newAccessToken = _tokenService.GenerateAccessToken(refreshToken.User);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                UserId = refreshToken.UserId,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return Result<TokenResponseDto>.Success(new TokenResponseDto(newAccessToken, newRefreshToken));
        }

        public async Task<Result<bool>> ForgotPasswordAsync(ForgotPasswordDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<bool>.Success(true);
            }

            var resetToken = _tokenService.GenerateRefreshToken();

            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);

            await _userRepository.SaveChangesAsync();
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetToken);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto request)
        {
            var user = await _userRepository.GetByPasswordResetTokenAsync(request.Token);

            if (user == null || user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                return Result<bool>.Failure("Invalid or expired token.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;

            var activeTokens = await _refreshTokenRepository.GetActiveByUserIdAsync(user.Id);

            foreach (var token in activeTokens)
            {
                token.IsRevoked = true;
            }

            await _refreshTokenRepository.SaveChangesAsync();
            await _userRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}