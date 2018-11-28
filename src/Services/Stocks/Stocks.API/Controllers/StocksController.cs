using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StockExchangeAnalyzer.Services.Stocks.API.Controllers
{
    using Application.Queries;

    [Route("api/v1/[controller]")]
    public class StocksController : Controller
    {
        readonly IStockQueries _stockQueries;

        public StocksController(IStockQueries stockQueries)
        {
            _stockQueries = stockQueries;
        }

        [Route("quotations/{isin}")]
        [HttpGet]
        public async Task<IActionResult> GetStockQuotationsAsync(string isin)
        {
            var stocks = await _stockQueries.GetStockQuotationsAsync(isin);
            return Ok(stocks);
        }

        [Route("top10-gaining")]
        [HttpGet]
        public async Task<IActionResult> GetTop10GainingStocksAsync()
        {
            var stocks = await _stockQueries.GetTop10GainingStocksAsync();
            return Ok(stocks);
        }

        [Route("top10-declaining")]
        [HttpGet]
        public async Task<IActionResult> GetTop10DeclainingStocksAsync()
        {
            var stocks = await _stockQueries.GetTop10DeclainingStocksAsync();
            return Ok(stocks);
        }

        [Route("top10-most-active")]
        [HttpGet]
        public async Task<IActionResult> GetTop10MostActiveStocksAsync()
        {
            var stocks = await _stockQueries.GetTop10MostActiveStocksAsync();
            return Ok(stocks);
        }
    }
}
