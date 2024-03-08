using FinanceTracker.Data;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Comment>> GetAll()
        {
            return await _appDbContext.Comments.Include(s => s.AppUser).ToListAsync();
        }

        public override async Task<Comment?> GetById(int id)
        {
            return await _appDbContext.Comments.Include(s => s.AppUser).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Comment>> GetByStockID(int stockID)
        {
            var comments = await _appDbContext.Comments.Include(s => s.AppUser).Where(c => c.StockId == stockID).ToListAsync();
            return comments;
        }

    }
}
