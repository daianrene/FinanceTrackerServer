using FinanceTracker.Models;

namespace FinanceTracker.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        public Task<List<Comment>> GetByStockID(int stockID);

    };
}
