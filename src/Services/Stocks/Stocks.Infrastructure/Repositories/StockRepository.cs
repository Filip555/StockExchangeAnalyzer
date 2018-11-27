using System;
using System.Threading.Tasks;
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

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock> GetAsync(string isin)
        {
            return await _context.Stocks
                .Include(x => x.Quotations)
                .SingleAsync(x => x.Isin == isin);
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
