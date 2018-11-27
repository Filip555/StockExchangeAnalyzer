namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    using Common.Application.Commands;

    public class AddStockCommand : ICommand
    {
        public AddStockCommand(string isin, string name)
        {
            Isin = isin;
            Name = name;
        }

        public string Isin { get; }
        public string Name { get; }
    }
}
