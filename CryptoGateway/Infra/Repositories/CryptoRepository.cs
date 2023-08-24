using CryptoGateway.Domain.Contracts;
using CryptoGateway.Domain.Entities;

namespace CryptoGateway.Infra.Repositories;

public class CryptoRepository : ICryptoRepository
{
    private readonly List<Cryptocurrency> _cryptos;

    public CryptoRepository()
    {
        Exchange binance = new("Binance", "https://api.binance.com");
        Exchange kucoin = new("Kucoin", "https://api.kucoin.com");

        Cryptocurrency bitcoin = new("Bitcoin", "BTC");
        bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(binance.Id, "BTCUSDT"));
        bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(kucoin.Id, "BTC-USDT"));
        
        _cryptos = new List<Cryptocurrency>()
        {
            bitcoin,
        };
    }
    
    public Cryptocurrency? GetBySymbol(string symbol)
    {
        return _cryptos.SingleOrDefault(c => c.Symbol == symbol);
    }
}