using System.Text.Json.Serialization;
using CryptoGateway;
using CryptoGateway.Domain.Contracts;
using CryptoGateway.Infra;
using CryptoGateway.Infra.Repositories;
using CryptoGateway.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CryptoGatewayContext>(
    options => options.UseSqlite(@"Data Source=TempDB/CryptoGateway.db;"));

builder.Services.AddHttpClient();
builder.Services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
builder.Services.AddTransient<ICryptoPriceService, CryptoPriceService>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InitializeDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }