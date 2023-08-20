using CryptoGateway.Shared;

namespace CryptoGateway.Adapter.Binance;

public sealed class BinanceExchangeAdapter : IExchangeAdapter<BinanceCryptoRequest>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BinanceExchangeAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ExchangeResponse> GetCryptoPriceAsync(BinanceCryptoRequest request)
    {
        using var client = _httpClientFactory.CreateClient();
        
        var uri = new Uri($"https://api.binance.com/api/v3/ticker/24hr?symbol={request.CryptoSymbol}");
        
        var crypto = await client.GetFromJsonAsync<BinanceCryptoResponse>(uri);
       
        return new ExchangeResponse(
            request.Name,
            crypto?.Symbol ?? request.CryptoSymbol,
            crypto?.AskPrice ?? 0M);
    }
}