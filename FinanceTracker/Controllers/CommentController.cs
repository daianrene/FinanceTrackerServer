using api.Dtos.Comment;
using FinanceTracker.Dto.Comment;
using FinanceTracker.Mapper;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMPService fmpService)
        {
            _commentRepository = commentRepo;
            _stockRepository = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAll();
            var commmentsDto = comments.Select(c => c.ToCommentDto());

            return Ok(commmentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequest comment)
        {

            var stock = await _stockRepository.GetBySymbol(symbol);

            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbol(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepository.Create(stock);
                };
            }

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByNameAsync(userName!);

            var commentModel = comment.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;

            await _commentRepository.Create(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest comment)
        {
            var commentModel = await _commentRepository.GetById(id);

            if (commentModel == null)
            {
                return NotFound();
            }

            commentModel.Title = comment.Title;
            commentModel.Content = comment.Content;

            await _commentRepository.Update(commentModel);

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //Expression<Func<Comment, bool>> condition = x => x.Id == id;

            if (!await _commentRepository.CheckIfExists(x => x.Id == id))
            {
                return NotFound();
            }

            await _commentRepository.Delete(id);

            return NoContent();
        }
    }
}
