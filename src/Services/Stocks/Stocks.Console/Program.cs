using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.CommandLineUtils;

namespace StockExchangeAnalyzer.Services.Stocks.Console
{
    using Application.Commands;
    using Domain.Model;
    using Infrastructure.Repositories;
    using Infrastructure.Services;
    using Infrastructure;

    class Program
    {
        static ServiceProvider _serviceProvider;
        static CommandLineApplication _commandLine;

        static Program()
        {
            ConfigureServices();
            ConfigureCommandLine();
        }

        static void Main(string[] args)
        {
            try
            {
                _commandLine.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Unable to execute command: {0}", ex.Message);
            }
        }

        private static void ConfigureCommandLine()
        {
            _commandLine = new CommandLineApplication
            {
                Name = "Stocks.Console",
                Description = ".NET Core console application for downloading stocks and quotations.",
            };
            _commandLine.HelpOption("-?|-h|--help");
            _commandLine.VersionOption("-v|--version", () =>
            {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });
            _commandLine.OnExecute(() =>
            {
                _commandLine.ShowHint();
                return 0;
            });
            _commandLine.Command("full-import", (command) =>
            {
                command.Description = "Downloads and replaces all stocks and quotiations.";
                command.OnExecute(async() =>
                {
                    await _serviceProvider.GetService<IStockCommands>().ExecuteAsync(new FullImportCommand());
                    return 0;
                });
            });
            _commandLine.Command("incremental-import", (command) =>
            {
                command.Description = "Downloads and adds new stocks and quotations.";
                command.OnExecute(async() =>
                {
                    await _serviceProvider.GetService<IStockCommands>().ExecuteAsync(new IncrementalImportCommand());
                    return 0;
                });
            });
        }

        private static void ConfigureServices()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<IStockCommands, StockCommands>()
                .AddTransient<IStockRepository, StockRepository>()
                .AddTransient<IStockService, StockService>()
                .AddTransient(x => { return new StockContext("Data Source=KOMPUTER\\SQLEXPRESS;Initial Catalog=Stock;Persist Security Info=True;User Id=stock;Password=stock;"); })
                .BuildServiceProvider();
        }
    }
}
