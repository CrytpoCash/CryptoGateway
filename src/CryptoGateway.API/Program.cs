using System.Text.Json.Serialization;
using CryptoGateway.API;
using CryptoGateway.Application.Services;
using CryptoGateway.Domain.Contracts;
using CryptoGateway.Infra;
using CryptoGateway.Infra.Adapters.Binance;
using CryptoGateway.Infra.Adapters.Kucoin;
using CryptoGateway.Infra.Factories;
using CryptoGateway.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

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
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
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