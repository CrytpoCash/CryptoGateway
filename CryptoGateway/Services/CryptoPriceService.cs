using CryptoGateway.Adapter.Binance;
using CryptoGateway.Adapter.Kucoin;
using CryptoGateway.Shared;

namespace CryptoGateway.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly BinanceExchangeAdapter _binanceExchangeAdapter;
    private readonly KucoinExchangeAdapter _kucoinExchangeAdapter;

    public CryptoPriceService(BinanceExchangeAdapter binanceExchangeAdapter, KucoinExchangeAdapter kucoinExchangeAdapter)
    {
        _binanceExchangeAdapter = binanceExchangeAdapter;
        _kucoinExchangeAdapter = kucoinExchangeAdapter;
    }

    public IEnumerable<ExchangeResponse> GetCryptoPrice(string symbol)
    {
        var taskBinance = _binanceExchangeAdapter.GetCryptoPriceAsync(new("Binance", "BTCUSDT"));
        
        var taskKucoin = _kucoinExchangeAdapter.GetCryptoPriceAsync(new("Kucoin", "BTC-USDT"));

        Task.WaitAll(new Task[]{ taskBinance, taskKucoin });
        
        var exchangeResponses = new ExchangeResponse[2]
        {
            taskBinance.Result,
            taskKucoin.Result
        };
        
        return exchangeResponses;
    }
}