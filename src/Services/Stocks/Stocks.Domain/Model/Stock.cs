using System;
using System.Collections.Generic;

namespace StockExchangeAnalyzer.Services.Stocks.Domain.Model
{
    using Common.Domain.Model;

    public class Stock : AggregateRoot
    {
        readonly List<StockQuotation> _quotations;

        public Stock(string isin, string name)
        {
            Isin = isin;
            Name = name;
            _quotations = new List<StockQuotation>();
        }

        public string Isin { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<StockQuotation> Quotations => _quotations;

        public void AddQuotation(
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
            var quotation = new StockQuotation(
                Isin,
                date,
                open,
                close,
                low,
                high,
                change,
                volume,
                value,
                transactions);
            _quotations.Add(quotation);
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Isin;
        }
    }
}
