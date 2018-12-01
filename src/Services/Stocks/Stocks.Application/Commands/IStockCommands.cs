using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    public interface IStockCommands
    {
        Task ExecuteAsync(FullImportCommand command);
        Task ExecuteAsync(IncrementalImportCommand command);
    }
}
