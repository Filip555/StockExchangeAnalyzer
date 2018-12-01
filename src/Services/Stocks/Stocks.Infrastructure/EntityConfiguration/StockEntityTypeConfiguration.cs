using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure.EntityConfiguration
{
    using Domain.Model;

    public class StockEntityTypeConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> configuration)
        {
            configuration
                .HasKey(x => x.Isin);
            configuration
                .Property(x => x.Isin)
                .HasMaxLength(12);
            configuration
                .Property(x => x.Name)
                .HasMaxLength(12)
                .IsRequired();
            configuration
                .HasMany(x => x.Quotations)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}