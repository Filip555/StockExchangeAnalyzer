using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Domain.Model
{
    using Common.Domain.Model;

    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock> GetAsync(string isin);
        Task UpdateAsync(Stock stock);
        Task AddAsync(Stock stock);
    }
}
