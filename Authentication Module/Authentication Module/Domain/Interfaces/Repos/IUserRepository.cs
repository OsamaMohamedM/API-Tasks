using Authentication_Module.Domain.Entities;

namespace Authentication_Module.Domain.Interfaces.Repos
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string username);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByPasswordResetTokenAsync(string token);

        Task AddAsync(User user);

        Task SaveChangesAsync();
    }
}