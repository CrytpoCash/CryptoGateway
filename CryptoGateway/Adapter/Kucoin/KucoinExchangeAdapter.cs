using CryptoGateway.Shared;

namespace CryptoGateway.Adapter.Kucoin;

public sealed class KucoinExchangeAdapter : IExchangeAdapter<KucoinCryptoRequest>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public KucoinExchangeAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ExchangeResponse> GetCryptoPriceAsync(KucoinCryptoRequest request)
    {
        using var client = _httpClientFactory.CreateClient();
        
        var uri = new Uri($"https://api.kucoin.com/api/v1/market/stats?symbol={request.CryptoSymbol}");
        
        var crypto = await client.GetFromJsonAsync<KucoinCryptoData>(uri);
       
        return new ExchangeResponse(
            request.Name,
            crypto?.Data?.Symbol ?? request.CryptoSymbol,
            crypto?.Data?.Last ?? 0M);
    }
}