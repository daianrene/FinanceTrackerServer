using FinanceTracker.Data;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        private readonly AppDbContext _appDbContext;
        public StockRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Stock>> GetAll()
        {
            return await _appDbContext.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).ToListAsync();
        }

        public override async Task<Stock?> GetById(int id)
        {
            return await _appDbContext.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> GetBySymbol(string symbol)
        {
            return await _appDbContext.Stocks.FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
        }
    }
}
