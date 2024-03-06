using FinanceTracker.Data;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _appDbContext;
        public PortfolioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _appDbContext.Portfolios.AddAsync(portfolio);
            await _appDbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task DeletePortfolio(AppUser user, string symbol)
        {
            var portfolioModel = await _appDbContext.Portfolios
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel != null)
            {
                _appDbContext.Portfolios.Remove(portfolioModel);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _appDbContext.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(s => new Stock
                {
                    Id = s.StockId,
                    Symbol = s.Stock.Symbol,
                    Company = s.Stock.Company,
                    Purchase = s.Stock.Purchase,
                    LastDiv = s.Stock.LastDiv,
                    Industry = s.Stock.Industry,
                    MarketCap = s.Stock.MarketCap
                }).ToListAsync();
        }
    }
}
