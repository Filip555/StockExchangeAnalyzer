using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace StockExchangeAnalyzer.Services.Stocks.API
{
    using Application.Commands;
    using Application.Queries;
    using Domain.Model;
    using Infrastructure;
    using Infrastructure.Repositories;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStockCommands, StockCommands>();
            services.AddTransient<IStockQueries, StockQueries>(x => { return new StockQueries(Configuration.GetConnectionString("Stock")); });
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<StockContext>(options =>
                   {
                       options.UseSqlServer(Configuration.GetConnectionString("Stock"),
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                   );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Stocks API", Version = "v1" });
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            app.UseMvc();
        }
    }
}
