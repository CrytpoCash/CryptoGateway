using CryptoGateway.Shared;

namespace CryptoGateway.Services;

public interface ICryptoPriceService
{
    IEnumerable<ExchangeResponse> GetCryptoPrice(string symbol);
}