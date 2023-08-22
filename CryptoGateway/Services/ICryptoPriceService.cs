using CryptoGateway.Shared;

namespace CryptoGateway.Services;

public interface ICryptoPriceService
{
    Task<IEnumerable<ExchangeResponse>> GetCryptoPriceAsync(string symbol);
}