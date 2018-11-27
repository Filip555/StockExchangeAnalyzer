using System;
using System.Collections.Generic;

namespace StockExchangeAnalyzer.Services.Stocks.Domain.Model
{
    using Common.Domain.Model;

    public class StockQuotation : ValueObject
    {
        public StockQuotation(
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

        public string Isin { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Open { get; private set; }
        public decimal Close { get; private set; }
        public decimal Low { get; private set; }
        public decimal High { get; private set; }
        public decimal Change { get; private set; }
        public uint Volume { get; private set; }
        public decimal Value { get; private set; }
        public uint Transactions { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Isin;
            yield return Date;
            yield return Open;
            yield return Close;
            yield return Low;
            yield return High;
            yield return Change;
            yield return Volume;
            yield return Value;
            yield return Transactions;
        }
    }
}
