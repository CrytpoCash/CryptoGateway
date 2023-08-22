namespace CryptoGateway.Shared;

public interface IExchangeApi
{
    static string BaseUrl { get; }
    Task<ExchangeResponse> GetCryptoPriceAsync(string cryptoSymbol);
}