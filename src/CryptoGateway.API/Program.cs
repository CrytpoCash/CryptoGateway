using System.Text.Json.Serialization;
using CriptoGateway.Infra;
using CriptoGateway.Infra.Adapters.Binance;
using CriptoGateway.Infra.Adapters.Kucoin;
using CriptoGateway.Infra.Factories;
using CriptoGateway.Infra.Repositories;
using CryptoGateway.API;
using CryptoGateway.Application.Services;
using CryptoGateway.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CryptoGatewayContext>(
    options => options.UseSqlite(@"Data Source=TempDB/CryptoGateway.API.db;"));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IExchangeApiAdapterFactory, ExchangeApiAdapterFactory>();
builder.Services.AddSingleton<BinanceExchangeAdapter>();
builder.Services.AddSingleton<KucoinExchangeAdapter>();
builder.Services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
builder.Services.AddTransient<ICryptoPriceService, CryptoPriceService>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.InitializeDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace CryptoGateway.API
{
    public partial class Program { }
}