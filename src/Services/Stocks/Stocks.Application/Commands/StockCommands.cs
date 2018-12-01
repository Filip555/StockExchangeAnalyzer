using System;
using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    using Domain.Model;

    public class StockCommands : IStockCommands
    {
        readonly IStockRepository _stockRepository;
        readonly IStockService _stockService;

        public StockCommands(
            IStockRepository stockRepository, 
            IStockService stockService)
        {
            _stockRepository = stockRepository;
            _stockService = stockService;
        }

        public async Task ExecuteAsync(FullImportCommand command)
        {
            var fromDate = new DateTime(2016, 1, 1);
            var stocks = await _stockService.DownloadAsync(fromDate);
            await _stockRepository.RemoveAllAsync();
            await _stockRepository.AddAllAsync(stocks);
        }

        public async Task ExecuteAsync(IncrementalImportCommand command)
        {
            var lastUpdated = await _stockRepository.GetLastUpdatedAsync();
            var fromDate = lastUpdated?.AddDays(1) ?? new DateTime(2016, 1, 1);
            var stocks = await _stockService.DownloadAsync(fromDate);
            await _stockRepository.UpdateAllAsync(stocks);
        }
    }
}
