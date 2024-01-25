using FinanceTracker.DTO.Stock;
using FinanceTracker.Mapper;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAll();
            var stocksDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetById(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest stock)
        {
            var stockModel = stock.ToStockFromCreateDto();
            await _stockRepo.Create(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest stock)
        {
            var stockModel = await _stockRepo.GetById(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = stock.Symbol;
            stockModel.MarketCap = stock.MarketCap;
            stockModel.Company = stock.Company;
            stockModel.Purchase = stock.Purchase;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.Industry = stock.Industry;
            stockModel.MarketCap = stock.MarketCap;

            await _stockRepo.Update(stockModel);

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            Expression<Func<Stock, bool>> condition = x => x.Id == id;

            if (!await _stockRepo.CheckIfExists(condition))
            {
                return NotFound();
            }

            await _stockRepo.Delete(id);

            return NoContent();
        }
    }
}
