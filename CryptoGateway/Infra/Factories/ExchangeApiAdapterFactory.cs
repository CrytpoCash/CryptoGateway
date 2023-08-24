﻿using CryptoGateway.Infra.Adapter.Binance;
using CryptoGateway.Infra.Adapter.Kucoin;
using CryptoGateway.Shared;

namespace CryptoGateway.Infra.Factories;

public interface IExchangeApiAdapterFactory
{
    IExchangeApi CreateAdapter(string baseUrl);
}

public class ExchangeApiAdapterFactory : IExchangeApiAdapterFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceProvider _serviceProvider;

    public ExchangeApiAdapterFactory(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _httpClientFactory = httpClientFactory;
        _serviceProvider = serviceProvider;
    }
    
    public IExchangeApi CreateAdapter(string baseUrl)
    {
        if (baseUrl == BinanceExchangeAdapter.BaseUrl)
        {
            return _serviceProvider.GetRequiredService<BinanceExchangeAdapter>();
        }
        else if (baseUrl == KucoinExchangeAdapter.BaseUrl)
        {
            return _serviceProvider.GetRequiredService<KucoinExchangeAdapter>();
        }
        
        throw new Exception("Exchange with unsupported base URL.");
    }
}