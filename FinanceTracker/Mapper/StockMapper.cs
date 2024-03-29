﻿using FinanceTracker.Dto.Stock;
using FinanceTracker.DTO.Stock;
using FinanceTracker.Models;

namespace FinanceTracker.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequest request)
        {
            return new Stock
            {
                Symbol = request.Symbol,
                Company = request.Company,
                Purchase = request.Purchase,
                LastDiv = request.LastDiv,
                Industry = request.Industry,
                MarketCap = request.MarketCap

            };
        }

        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                Company = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.price,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };
        }
    }
}
