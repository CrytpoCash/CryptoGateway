using CryptoGateway.Domain.Entities;

namespace CryptoGateway.Domain.Contracts;

public interface ICryptocurrencyRepository
{
    Task<Cryptocurrency?> GetBySymbolAsync(string symbol);
}