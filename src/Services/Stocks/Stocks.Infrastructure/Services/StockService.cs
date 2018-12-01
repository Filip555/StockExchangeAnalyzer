using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http;

namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure.Services
{
    using Domain.Model;

    public class StockService : IStockService
    {
        IDictionary<string, Stock> _stocks;

        public StockService()
        {
            _stocks = new Dictionary<string, Stock>();
        }

        public async Task<IEnumerable<Stock>> DownloadAsync(DateTime date)
        {
            while(date < DateTime.Now)
            {
                Console.WriteLine(date.ToShortDateString());
                var response = await GetResponseAsync(date);
                Parse(response, date);
                date = date.AddDays(1);
            }
            return _stocks.Select(x => x.Value);
        }

        private async Task<string> GetResponseAsync(DateTime date)
        {
            var dateString = date.ToString("dd-MM-yyyy");
            var requestUri = $@"https://www.gpw.pl/archiwum-notowan-full?type=10&instrument=&date={dateString}";
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync(requestUri);
        }

        private void Parse(string html, DateTime date)
        {
            var regex = new Regex(@"<tr[^>]*>\s*(<td[^>]*>\s*([^<]*)\s*</td>\s*){11}</tr>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                var name = match.Groups[2].Captures[0].Value.Trim();
                var isin = match.Groups[2].Captures[1].Value.Trim();
                var open = match.Groups[2].Captures[3].Value.Trim().ToDecimal();
                var high = match.Groups[2].Captures[4].Value.Trim().ToDecimal();
                var low = match.Groups[2].Captures[5].Value.Trim().ToDecimal();
                var close = match.Groups[2].Captures[6].Value.Trim().ToDecimal();
                var change = match.Groups[2].Captures[7].Value.Trim().ToDecimal();
                var volume = match.Groups[2].Captures[8].Value.Trim().ToUInt();
                var transactions = match.Groups[2].Captures[9].Value.Trim().ToUInt();
                var value = match.Groups[2].Captures[10].Value.Trim().ToDecimal() * 1000;
                var stock = GetStock(isin, name);
                stock.AddQuotation(date, open, close, low, high, change, volume, value, transactions);
            }
        }

        private Stock GetStock(string isin, string name)
        {
            if (!_stocks.ContainsKey(isin))
            {
                _stocks.Add(isin, new Stock(isin, name));
            }
            return _stocks[isin];
        }
    }
}
