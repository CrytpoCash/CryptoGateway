using CryptoGateway.Domain.Entities;
using CryptoGateway.Infra;

namespace CryptoGateway.API;

public static class ApplicationInitializer
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetService<CryptoGatewayContext>();
        context?.Database.EnsureCreated();

        var existExchange = context.Exchanges.Any();
        
        if (!existExchange)
        {
            var binance = new Exchange("Binance", "https://api.binance.com");
            var kucoin = new Exchange("Kucoin", "https://api.kucoin.com");
                
            context.Exchanges.Add(binance);
            context.Exchanges.Add(kucoin);

            context.SaveChanges();
            
            var bitcoin = new Cryptocurrency("Bitcoin", "BTC");
            bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(binance.Id, "BTCUSDT"));
            bitcoin.AddExchangeItem(new CryptocurrencyExchangeItem(kucoin.Id, "BTC-USDT"));
        
            context.Cryptocurrencys.Add(bitcoin);

            context.SaveChanges();
        }
    }
}