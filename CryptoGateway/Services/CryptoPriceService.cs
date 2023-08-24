using CryptoGateway.Domain.Contracts;
using CryptoGateway.Infra.Factories;
using CryptoGateway.Shared;

namespace CryptoGateway.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoPriceService(ICryptocurrencyRepository cryptocurrencyRepository, IHttpClientFactory httpClientFactory)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ExchangeResponse>> GetCryptoPriceAsync(string symbol)
    {
        var cryptocurrency = await _cryptocurrencyRepository.GetBySymbolAsync(symbol);

        if (cryptocurrency is null || !cryptocurrency.ExchangeItems.Any()) 
        {
            return Enumerable.Empty<ExchangeResponse>();
        }

        var tasks = cryptocurrency.ExchangeItems.Select(item =>
        {
            var adapter = ExchangeApiAdapterFactory.CreateAdapter(_httpClientFactory, item.Exchange.BaseURL);
            return adapter.GetCryptoPriceAsync(item.CryptoSymbolExternal);
        });

        var exchangeResponses = await Task.WhenAll(tasks);
        
        return exchangeResponses;
    }
}