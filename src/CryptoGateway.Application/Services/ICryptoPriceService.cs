using CryptoGateway.Core.Adapter;

namespace CryptoGateway.Application.Services;

public interface ICryptoPriceService
{
    Task<IEnumerable<ExchangeResponse>> GetCryptoPriceAsync(string symbol);
}