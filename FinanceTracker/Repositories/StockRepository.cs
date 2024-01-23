using FinanceTracker.Data;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;

namespace FinanceTracker.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {

        public StockRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        /* public async Task<Stock> CreateAsync(Stock stock)
         {
             await _context.Stocks.AddAsync(stock);
             await _context.SaveChangesAsync();
             return stock;
         }

         public async Task<Stock?> DeleteAsync(int id)
         {
             var stockModel = await _context.Stocks.FindAsync(id);

             if (stockModel == null)
             {
                 return null;
             }

             _context.Stocks.Remove(stockModel);
             await _context.SaveChangesAsync();

             return stockModel;
         }

         public async Task<List<Stock>> GetAllAsync()
         {
             return await _context.Stocks.ToListAsync();
         }

         public async Task<Stock?> GetByIdAsync(int id)
         {
             return await _context.Stocks.FindAsync(id);
         }

         public async Task<Stock?> UpdateAsync(int id, Stock stock)
         {
             var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

             if (stockModel == null)
             {
                 return null;
             }

             stockModel.Symbol = stock.Symbol;
             stockModel.MarketCap = stock.MarketCap;
             stockModel.Company = stock.Company;
             stockModel.Purchase = stock.Purchase;
             stockModel.LastDiv = stock.LastDiv;
             stockModel.Industry = stock.Industry;
             stockModel.MarketCap = stock.MarketCap;

             await _context.SaveChangesAsync();

             return stockModel;
         }*/
    }
}
