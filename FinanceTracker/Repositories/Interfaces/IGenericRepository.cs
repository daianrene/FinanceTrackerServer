using System.Linq.Expressions;

namespace FinanceTracker.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);

        Task<bool> CheckIfExists(Expression<Func<T, bool>> expr);

    }
}
