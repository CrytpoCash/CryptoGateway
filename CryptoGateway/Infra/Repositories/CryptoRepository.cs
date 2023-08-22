using CryptoGateway.Domain.Contracts;
using CryptoGateway.Domain.Entities;

namespace CryptoGateway.Infra.Repositories;

public class CryptoRepository : ICryptoRepository
{
    private readonly List<Crypto> _cryptos;

    public CryptoRepository()
    {
        Exchange binance = new("Binance", "https://api.binance.com");
        Exchange kucoin = new("Kucoin", "https://api.kucoin.com");

        Crypto bitcoin = new("Bitcoin", "BTC");
        bitcoin.AddCryptoSymbolExchange(new CryptoSymbolExchange(binance, "BTCUSDT"));
        bitcoin.AddCryptoSymbolExchange(new CryptoSymbolExchange(kucoin, "BTC-USDT"));
        
        _cryptos = new List<Crypto>()
        {
            bitcoin,
        };
    }
    
    public Crypto? GetBySymbol(string symbol)
    {
        return _cryptos.SingleOrDefault(c => c.Symbol == symbol);
    }
}