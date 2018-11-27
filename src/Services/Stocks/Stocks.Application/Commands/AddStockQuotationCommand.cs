using System;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Commands
{
    using Common.Application.Commands;

    public class AddStockQuotationCommand : ICommand
    {
        public AddStockQuotationCommand(
            string isin,
            DateTime date,
            decimal open,
            decimal close,
            decimal low,
            decimal high,
            decimal change,
            uint volume,
            decimal value,
            uint transactions)
        {
            Isin = isin;
            Date = date;
            Open = open;
            Close = close;
            Low = low;
            High = high;
            Change = change;
            Volume = volume;
            Value = value;
            Transactions = transactions;
        }

        public string Isin { get; }
        public DateTime Date { get; }
        public decimal Open { get; }
        public decimal Close { get; }
        public decimal Low { get; }
        public decimal High { get; }
        public decimal Change { get; }
        public uint Volume { get; }
        public decimal Value { get; }
        public uint Transactions { get; }
    }
}
