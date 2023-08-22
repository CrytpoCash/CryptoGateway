using CryptoGateway.Infra.Adapter.Binance;
using CryptoGateway.Infra.Adapter.Kucoin;
using CryptoGateway.Shared;

namespace CryptoGateway.Infra.Factories;

public static class ExchangeApiAdapterFactory
{
    public static IExchangeApi CreateAdapter(IHttpClientFactory httpClientFactory, string baseUrl)
    {
        if (baseUrl == BinanceExchangeAdapter.BaseUrl)
        {
            return new BinanceExchangeAdapter(httpClientFactory);
        }
        else 
        if (baseUrl == KucoinExchangeAdapter.BaseUrl)
        {
            return new KucoinExchangeAdapter(httpClientFactory);
        }
        
        throw new Exception("Exchange with unsupported base URL.");
    }
}