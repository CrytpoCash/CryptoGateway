namespace CryptoGateway.Shared;

public interface IExchangeAdapter<T> where T : ExchangeRequest
{
    Task<ExchangeResponse> GetCryptoPriceAsync(T request);
}