namespace StockExchangeAnalyzer.Services.Stocks.Infrastructure.Services
{
    public static class StockServiceExtensions
    {
        public static decimal ToDecimal(this string source)
        {
            source = source.Replace(" ", "").Replace(".", "");
            if (decimal.TryParse(source, out var result))
            {
                return result;
            }
            return default(decimal);
        }

        public static uint ToUInt(this string source)
        {
            source = source.Replace(" ", "").Replace(".", "");
            if (uint.TryParse(source, out var result))
            {
                return result;
            }
            return default(uint);
        }
    }
}
