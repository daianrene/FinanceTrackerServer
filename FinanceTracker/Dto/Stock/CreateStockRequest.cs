using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DTO.Stock
{
    public class CreateStockRequest
    {
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        public string Company { get; set; } = string.Empty;
        [Required]
        public decimal Purchase { get; set; }
        [Required]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Required]
        public long MarketCap { get; set; }
    }
}
