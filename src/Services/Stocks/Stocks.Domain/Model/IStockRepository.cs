using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StockExchangeAnalyzer.Services.Stocks.Domain.Model
{
    using Common.Domain.Model;
    
    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock> GetAsync(string isin);
        Task AddAsync(Stock stock);
        Task UpdateAllAsync(IEnumerable<Stock> stocks);
        Task AddAllAsync(IEnumerable<Stock> stocks);
        Task RemoveAllAsync();
        Task<DateTime?> GetLastUpdatedAsync();
    }
}
