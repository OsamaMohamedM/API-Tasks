using Authentication_Module.Domain.Entities;

namespace Authentication_Module.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);

        public string GenerateRefreshToken();
    }
}