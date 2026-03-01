using Authentication_Module.Domain.Entities;

namespace Authentication_Module.Domain.Interfaces.Repos
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string username);

        Task AddAsync(User user);

        Task SaveChangesAsync();
    }
}