using FinanceTracker.Controllers;
using FinanceTracker.DTO.Stock;
using FinanceTracker.Mapper;
using FinanceTracker.Models;
using FinanceTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace FinanceTracker.Test
{
    public class StockControllerTests
    {
        private readonly Mock<IStockRepository> _mockStockRepository;
        private readonly StockController _controller;

        public StockControllerTests()
        {
            _mockStockRepository = new Mock<IStockRepository>();
            _controller = new StockController(_mockStockRepository.Object);
        }

        [Fact]
        public async Task GetAll_WhenStocksExist_ReturnsOkResultWithStocks()
        {
            // Arrange
            var stocks = new List<Stock>
            {
                new Stock { Id = 1, Symbol = "AAPL", Company = "Apple Inc.", MarketCap = 200000000000 },
                new Stock { Id = 2, Symbol = "MSFT", Company = "Microsoft Corporation", MarketCap = 150000000000 }
            };
            _mockStockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(stocks);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStocks = Assert.IsAssignableFrom<IEnumerable<StockDto>>(okResult.Value);
            Assert.Equal(2, returnedStocks.Count());
        }

        [Fact]
        public async Task GetById_WhenValidId_ReturnsOkResultWithStockDto()
        {
            // Arrange
            var stockId = 1;
            var stock = new Stock { Id = stockId, Symbol = "AAPL", Company = "Apple Inc.", MarketCap = 200000000000 };
            _mockStockRepository.Setup(repo => repo.GetById(stockId)).ReturnsAsync(stock);

            // Act
            var result = await _controller.GetById(stockId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStock = Assert.IsType<StockDto>(okResult.Value);
            Assert.Equal(stockId, returnedStock.Id);
        }

        [Fact]
        public async Task Create_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var createStockRequest = new CreateStockRequest { Symbol = "AAPL", Company = "Apple Inc.", MarketCap = 200000000000 };
            var stock = createStockRequest.ToStockFromCreateDto();
            _mockStockRepository.Setup(repo => repo.Create(stock)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(createStockRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Update_ExistingStock_ReturnsOkResultWithUpdatedStockDto()
        {
            // Arrange
            var stockId = 1;
            var updateStockRequest = new UpdateStockRequest { Symbol = "AAPL", Company = "Apple Inc.", MarketCap = 220000000000 };
            var existingStock = new Stock { Id = stockId, Symbol = "AAPL", Company = "Apple Inc.", MarketCap = 200000000000 };
            _mockStockRepository.Setup(repo => repo.GetById(stockId)).ReturnsAsync(existingStock);
            _mockStockRepository.Setup(repo => repo.Update(existingStock)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(stockId, updateStockRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedStock = Assert.IsType<StockDto>(okResult.Value);
            Assert.Equal(updateStockRequest.MarketCap, updatedStock.MarketCap);
        }

        [Fact]
        public async Task Delete_ExistingStock_ReturnsNoContentResult()
        {
            // Arrange
            var stockId = 1;
            _mockStockRepository.Setup(repo => repo.CheckIfExists(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(true);
            _mockStockRepository.Setup(repo => repo.Delete(stockId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(stockId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistingStock_ReturnsNotFoundResult()
        {
            // Arrange
            var stockId = 1;
            _mockStockRepository.Setup(repo => repo.CheckIfExists(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(stockId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}