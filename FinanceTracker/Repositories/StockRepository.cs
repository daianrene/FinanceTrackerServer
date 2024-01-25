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
            return await _appDbContext.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public override async Task<Stock?> GetById(int id)
        {
            return await _appDbContext.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
