using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    using Domain.Model;

    public class StockCommands : IStockCommands
    {
        readonly IStockRepository _stockRepository;

        public StockCommands(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task ExecuteAsync(AddStockCommand command)
        {
            var stock = new Stock(command.Isin, command.Name);
            await _stockRepository.AddAsync(stock);
        }

        public async Task ExecuteAsync(AddStockQuotationCommand command)
        {
            var stock = await _stockRepository.GetAsync(command.Isin);
            stock.AddQuotation(
                command.Date,
                command.Open,
                command.Close,
                command.Low,
                command.High,
                command.Change,
                command.Volume,
                command.Value,
                command.Transactions);
            await _stockRepository.UpdateAsync(stock);
        }
    }
}
