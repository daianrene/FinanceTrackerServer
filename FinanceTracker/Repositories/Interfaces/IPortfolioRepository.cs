using FinanceTracker.Models;

namespace FinanceTracker.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task DeletePortfolio(AppUser user, string symbol);
    }
}
