using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Queries
{
    public interface IStockQueries
    {
        Task<IEnumerable<dynamic>> GetStockListAsync();
        Task<IEnumerable<dynamic>> GetStockQuotationsAsync(string isin);
        Task<IEnumerable<dynamic>> GetTop10GainingStocksAsync();
        Task<IEnumerable<dynamic>> GetTop10DeclainingStocksAsync();
        Task<IEnumerable<dynamic>> GetTop10MostActiveStocksAsync();
    }
}
