using CryptoGateway.Shared;

namespace CryptoGateway.Infra.Adapter.Kucoin;

public sealed class KucoinExchangeAdapter : IExchangeApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    public KucoinExchangeAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public static string BaseUrl => "https://api.kucoin.com";

    public async Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol)
    {
        using var client = _httpClientFactory.CreateClient();
        
        var uri = new Uri($"{BaseUrl}/api/v1/market/stats?symbol={cryptoSymbol}");
        
        var crypto = await client.GetFromJsonAsync<KucoinCryptoData>(uri);
       
        return new ExchangeResponse(
            crypto?.Data?.Symbol ?? cryptoSymbol,
            crypto?.Data?.Last ?? 0M);
    }
}