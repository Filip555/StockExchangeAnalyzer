using Microsoft.EntityFrameworkCore;

namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure
{
    using Domain.Model;
    using EntityConfiguration;

    public class StockContext : DbContext
    {
        private readonly string _connectionString;

        public StockContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public StockContext(DbContextOptions<StockContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockQuotation> StockQuotations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StockQuotationEntityTypeConfiguration());
        }
    }
}
