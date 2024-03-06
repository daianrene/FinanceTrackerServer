using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByNameAsync(userName!);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser!);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string symbol)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByNameAsync(userName!);
            var stock = await _stockRepository.GetBySymbol(symbol);

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser!);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already added");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepository.CreatePortfolio(portfolioModel);

            if (portfolioModel == null) return StatusCode(500, "Could not create");
            else return Created();

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string symbol)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByNameAsync(userName!);

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());

            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, symbol);
                return NoContent();
            }
            else return BadRequest("Stock not in your portfolio");
        }
    }
}
