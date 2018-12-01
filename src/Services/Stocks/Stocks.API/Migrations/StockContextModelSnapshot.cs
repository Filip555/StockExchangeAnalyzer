﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockExchangeAnalyzer.Services.Stocks.Infrastructure;

namespace Stocks.API.Migrations
{
    [DbContext(typeof(StockContext))]
    partial class StockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StockExchangeAnalyzer.Services.Stocks.Domain.Model.Stock", b =>
                {
                    b.Property<string>("Isin")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.HasKey("Isin");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockExchangeAnalyzer.Services.Stocks.Domain.Model.StockQuotation", b =>
                {
                    b.Property<string>("Isin")
                        .HasMaxLength(12);

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Change")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(10,2)");

                    b.Property<long>("Transactions");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(16,2)");

                    b.Property<long>("Volume");

                    b.HasKey("Isin", "Date");

                    b.ToTable("StockQuotations");
                });

            modelBuilder.Entity("StockExchangeAnalyzer.Services.Stocks.Domain.Model.StockQuotation", b =>
                {
                    b.HasOne("StockExchangeAnalyzer.Services.Stocks.Domain.Model.Stock")
                        .WithMany("Quotations")
                        .HasForeignKey("Isin")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}