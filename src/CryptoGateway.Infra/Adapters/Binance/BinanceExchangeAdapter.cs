using System.Net.Http.Json;
using CryptoGateway.Core.Adapter;
using Microsoft.Extensions.Logging;

namespace CryptoGateway.Infra.Adapters.Binance;

public sealed class BinanceExchangeAdapter : IExchangeApi
{
    private readonly ILogger<BinanceExchangeAdapter> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public BinanceExchangeAdapter(IHttpClientFactory httpClientFactory, ILogger<BinanceExchangeAdapter> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public static string BaseUrl => "https://api.binance.com";
    public static string ExchangeName => "Binance";

    public async Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol)
    {
        if (string.IsNullOrWhiteSpace(cryptoSymbol))
        {
            throw new ArgumentException("Invalid cryptocurrency.");
        }
        
        _logger.LogInformation("Getting price of cryptocurrency {Symbol} on exchange {Exchange}",  cryptoSymbol, ExchangeName);

        try
        {
            using var client = _httpClientFactory.CreateClient("httpClientAdapter");

            var uri = new Uri($"{BaseUrl}/api/v3/ticker/24hr?symbol={cryptoSymbol}");

            var crypto = await client.GetFromJsonAsync<BinanceCryptoResponse>(uri);

            return new ExchangeResponse(
                crypto?.Symbol ?? cryptoSymbol,
                (crypto?.AskPrice ?? 0M).ToString("N2"),
                ExchangeName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting price of cryptocurrency {Symbol} on exchange {Exchange}",
                cryptoSymbol, ExchangeName);
            throw;
        }
    }
}