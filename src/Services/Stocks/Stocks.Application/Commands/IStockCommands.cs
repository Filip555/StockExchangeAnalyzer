using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    public interface IStockCommands
    {
        Task ExecuteAsync(AddStockCommand command);
        Task ExecuteAsync(AddStockQuotationCommand command);
    }
}
