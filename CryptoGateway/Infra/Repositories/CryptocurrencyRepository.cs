using CryptoGateway.Domain.Contracts;
using CryptoGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoGateway.Infra.Repositories;

public class CryptocurrencyRepository : ICryptocurrencyRepository
{
    private readonly CryptoGatewayContext _context;
    public CryptocurrencyRepository(CryptoGatewayContext context)
    {
        _context = context;
        
        /*
        Exchange binance = new("Binance", "https://api.binance.com");
        Exchange kucoin = new("Kucoin", "https://api.kucoin.com");

        Cryptocurrency bitcoin = new("Bitcoin", "BTC");
        bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(binance.Id, "BTCUSDT"));
        bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(kucoin.Id, "BTC-USDT"));
        
        _cryptos = new List<Cryptocurrency>()
        {
            bitcoin,
        };
        */
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