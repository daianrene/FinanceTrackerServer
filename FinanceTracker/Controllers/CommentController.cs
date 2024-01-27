using api.Dtos.Comment;
using FinanceTracker.Dto.Comment;
using FinanceTracker.Mapper;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAll();
            var commmentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(commmentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequest comment)
        {
            //Expression<Func<Stock, bool>> condition = x => x.Id == stockId;

            if (!await _stockRepo.CheckIfExists(x => x.Id == stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = comment.ToCommentFromCreate(stockId);
            await _commentRepo.Create(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest comment)
        {
            var commentModel = await _commentRepo.GetById(id);

            if (commentModel == null)
            {
                return NotFound();
            }

            commentModel.Title = comment.Title;
            commentModel.Content = comment.Content;

            await _commentRepo.Update(commentModel);

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //Expression<Func<Comment, bool>> condition = x => x.Id == id;

            if (!await _commentRepo.CheckIfExists(x => x.Id == id))
            {
                return NotFound();
            }

            await _commentRepo.Delete(id);

            return NoContent();
        }
    }
}
