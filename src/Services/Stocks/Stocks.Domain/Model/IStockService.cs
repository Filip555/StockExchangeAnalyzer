using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Domain.Model
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> DownloadAsync(DateTime fromDate);
    }
}
