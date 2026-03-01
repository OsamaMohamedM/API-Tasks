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

        public AuthService(IUserRepository userRepository, ITokenService tokenProvider, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
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
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Result<LoginResponseDto>.Failure("Invalid UserName or password.");
            }

            var token = _tokenService.GenerateAccessToken(user);

            return Result<LoginResponseDto>.Success(new LoginResponseDto(token));
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
    }
}