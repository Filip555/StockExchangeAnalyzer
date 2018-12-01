using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure.Repositories
{
    using Domain.Model;

    public class StockRepository : IStockRepository
    {
        readonly StockContext _context;

        public StockRepository(StockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Stock> GetAsync(string isin)
        {
            return await _context.Stocks
                .Include(x => x.Quotations)
                .SingleAsync(x => x.Isin == isin);
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAllAsync(IEnumerable<Stock> stocks)
        {
            await _context.StockQuotations.AddRangeAsync(stocks.SelectMany(x => x.Quotations));
            await _context.SaveChangesAsync();
        }

        public async Task AddAllAsync(IEnumerable<Stock> stocks)
        {
            await _context.Stocks.AddRangeAsync(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAllAsync()
        {
            var stocks = await _context.Stocks.ToListAsync();
            _context.Stocks.RemoveRange(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime?> GetLastUpdatedAsync()
        {
            if (!await _context.StockQuotations.AnyAsync()) return null;
            return await _context.StockQuotations.MaxAsync(x => x.Date);
        }
    }
}
