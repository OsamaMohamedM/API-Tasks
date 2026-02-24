using System.Linq.Expressions;
using Task1.Helpers;

namespace Task1.Repository
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<PagedResult<T>> GetPagedAsync(PaginationParams paginationParams);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}