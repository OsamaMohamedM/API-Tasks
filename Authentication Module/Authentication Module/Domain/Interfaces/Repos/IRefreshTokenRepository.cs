using Authentication_Module.Domain.Entities;

namespace Authentication_Module.Domain.Interfaces.Repos
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);

        Task AddAsync(RefreshToken refreshToken);

        Task SaveChangesAsync();
    }
}
