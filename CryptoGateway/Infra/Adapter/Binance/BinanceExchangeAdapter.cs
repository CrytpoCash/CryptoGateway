using CryptoGateway.Shared;

namespace CryptoGateway.Infra.Adapter.Binance;

public sealed class BinanceExchangeAdapter : IExchangeApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BinanceExchangeAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public static string BaseUrl => "https://api.binance.com";
    public static string ExchangeName => "Binance";

    public async Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol)
    {
        using var client = _httpClientFactory.CreateClient();
        
        var uri = new Uri($"{BaseUrl}/api/v3/ticker/24hr?symbol={cryptoSymbol}");
        
        var crypto = await client.GetFromJsonAsync<BinanceCryptoResponse>(uri);
       
        return new ExchangeResponse(
            crypto?.Symbol ?? cryptoSymbol,
            (crypto?.AskPrice ?? 0M).ToString("N2"),
            ExchangeName);
    }
}