using FinanceTracker.Models;

namespace FinanceTracker.Repositories.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock?> GetBySymbol(string symbol);
    }
}
