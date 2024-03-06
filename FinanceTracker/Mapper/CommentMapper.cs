using FinanceTracker.Dto.Comment;
using FinanceTracker.Models;

namespace FinanceTracker.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                Title = commentModel.Title,
                StockId = commentModel.StockId,
                CreatedBy = commentModel.AppUser.UserName!
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentRequest commentModel, int stockId)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId
            };
        }

    }
}
