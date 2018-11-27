using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure.EntityConfiguration
{
    using Domain.Model;

    public class StockQuotationEntityTypeConfiguration : IEntityTypeConfiguration<StockQuotation>
    {
        public void Configure(EntityTypeBuilder<StockQuotation> configuration)
        {
            configuration
                .HasKey(x => new { x.Isin, x.Date });
            configuration
                .Property(x => x.Isin)
                .HasMaxLength(12);
            configuration
                .Property(x => x.Date)
                .IsRequired();
            configuration
                .Property(x => x.Open)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            configuration
                .Property(x => x.Close)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            configuration
                .Property(x => x.Low)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            configuration
                .Property(x => x.High)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            configuration
                .Property(x => x.Change)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            configuration
                .Property(x => x.Volume)
                .IsRequired();
            configuration
                .Property(x => x.Value)
                .HasColumnType("decimal(16,2)")
                .IsRequired();
            configuration
                .Property(x => x.Transactions)
                .IsRequired();
        }
    }
}