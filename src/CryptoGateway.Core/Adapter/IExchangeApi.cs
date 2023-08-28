namespace CryptoGateway.Core.Adapter;

public interface IExchangeApi
{
    static string BaseUrl { get; }
    Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol);
}