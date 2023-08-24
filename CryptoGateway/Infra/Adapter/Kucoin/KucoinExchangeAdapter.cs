using CryptoGateway.Shared;

namespace CryptoGateway.Infra.Adapter.Kucoin;

public sealed class KucoinExchangeAdapter : IExchangeApi
{
    private readonly ILogger<KucoinExchangeAdapter> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public KucoinExchangeAdapter(IHttpClientFactory httpClientFactory, ILogger<KucoinExchangeAdapter> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public static string BaseUrl => "https://api.kucoin.com";
    public static string ExchangeName => "Kucoin";

    public async Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol)
    {
        if (string.IsNullOrWhiteSpace(cryptoSymbol))
        {
            throw new ArgumentException("Invalid cryptocurrency.");
        }
        
        _logger.LogInformation("Getting price of cryptocurrency {Symbol} on exchange {Exchange}",  cryptoSymbol, ExchangeName);

        try
        {
            using var client = _httpClientFactory.CreateClient();
        
            var uri = new Uri($"{BaseUrl}/api/v1/market/stats?symbol={cryptoSymbol}");
        
            var crypto = await client.GetFromJsonAsync<KucoinCryptoData>(uri);
       
            return new ExchangeResponse(
                crypto?.Data?.Symbol ?? cryptoSymbol,
                (crypto?.Data?.Last ?? 0M).ToString("N2"),
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