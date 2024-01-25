using FinanceTracker.Data;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;

namespace FinanceTracker.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
