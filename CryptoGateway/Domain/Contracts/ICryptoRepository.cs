using CryptoGateway.Domain.Entities;

namespace CryptoGateway.Domain.Contracts;

public interface ICryptoRepository
{
    Crypto? GetBySymbol(string symbol);
}