using CryptoGateway.Domain.Entities;

namespace CryptoGateway.Domain.Contracts;

public interface ICryptoRepository
{
    Cryptocurrency? GetBySymbol(string symbol);
}