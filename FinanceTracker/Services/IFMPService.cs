using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbol(string symbol);
    }
}
