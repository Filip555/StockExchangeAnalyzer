using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace StockExchangeAnalyzer.Services.Stocks.Application.Queries
{
    public class StockQueries : IStockQueries
    {
        readonly string _connectionString;

        public StockQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<dynamic>> GetStockListAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<dynamic>(@"
                    SELECT s.[Isin], s.[Name]
                    FROM [dbo].[Stocks] s");
            }
        }

        public async Task<IEnumerable<dynamic>> GetStockQuotationsAsync(string isin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<dynamic>(@"
                    SELECT q.[DateTime], q.[Open], q.[Close], q.[Low], q.[High], q.[Change], q.[Volume], q.[Value], q.[Transactions]
                    FROM [dbo].[StockQuotations] q
                    WHERE q.[Isin] = @isin
                    ORDER BY q.[DateTime]", new { isin });
            }
        }

        public async Task<IEnumerable<dynamic>> GetTop10DeclainingStocksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<dynamic>(@"
                    SELECT TOP(10) s.[Isin], s.[Name], q.[Open], q.[Close], q.[Low], q.[High], q.[Change], q.[Volume], q.[Value], q.[Transactions]
                    FROM [dbo].[Stocks] s
                    LEFT JOIN [dbo].[StockQuotations] AS q ON q.[Isin] = s.[Isin]
                    WHERE q.[DateTime] = (SELECT MAX(m.[DateTime]) FROM [dbo].[StockQuotations] m)
                    ORDER BY q.[Change] ASC");
            }
        }

        public async Task<IEnumerable<dynamic>> GetTop10GainingStocksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<dynamic>(@"
                    SELECT TOP(10) s.[Isin], s.[Name], q.[Open], q.[Close], q.[Low], q.[High], q.[Change], q.[Volume], q.[Value], q.[Transactions]
                    FROM [dbo].[Stocks] s
                    LEFT JOIN [dbo].[StockQuotations] AS q ON q.[Isin] = s.[Isin]
                    WHERE q.[DateTime] = (SELECT MAX(m.[DateTime]) FROM [dbo].[StockQuotations] m)
                    ORDER BY q.[Change] DESC");
            }
        }

        public async Task<IEnumerable<dynamic>> GetTop10MostActiveStocksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<dynamic>(@"
                    SELECT TOP(10) s.[Isin], s.[Name], q.[Open], q.[Close], q.[Low], q.[High], q.[Change], q.[Volume], q.[Value], q.[Transactions]
                    FROM [dbo].[Stocks] s
                    LEFT JOIN [dbo].[StockQuotations] AS q ON q.[Isin] = s.[Isin]
                    WHERE q.[DateTime] = (SELECT MAX(m.[DateTime]) FROM [dbo].[StockQuotations] m)
                    ORDER BY q.[Volume] DESC");
            }
        }
    }
}
