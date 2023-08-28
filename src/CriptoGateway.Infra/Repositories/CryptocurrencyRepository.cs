using CryptoGateway.Domain.Contracts;
using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CriptoGateway.Infra.Repositories;

public class CryptocurrencyRepository : ICryptocurrencyRepository
{
    private readonly CryptoGatewayContext _context;
    public CryptocurrencyRepository(CryptoGatewayContext context)
    {
        _context = context;
    }
    
    public async Task<Cryptocurrency?> GetBySymbolAsync(string symbol)
    {
        var cryptocurrency = await _context.Cryptocurrencys
            .Include(x => x.ExchangeItems)
            .ThenInclude(e => e.Exchange)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Symbol == symbol);

        return cryptocurrency;
    }
}