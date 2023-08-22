using CryptoGateway.Domain.Contracts;
using CryptoGateway.Infra.Factories;
using CryptoGateway.Shared;

namespace CryptoGateway.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly ICryptoRepository _cryptoRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoPriceService(ICryptoRepository cryptoRepository, IHttpClientFactory httpClientFactory)
    {
        _cryptoRepository = cryptoRepository;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ExchangeResponse>> GetCryptoPriceAsync(string symbol)
    {
        var crypto = _cryptoRepository.GetBySymbol(symbol);

        if (crypto is null)
        {
            return Enumerable.Empty<ExchangeResponse>();
        }

        var tasks = crypto.CryptoSymbolExchanges.Select(item =>
        {
            var adapter = ExchangeApiAdapterFactory.CreateAdapter(_httpClientFactory, item.Exchange.BaseURL);
            return adapter.GetCryptoPriceAsync(item.CryptoSymbol);
        });

        var exchangeResponses = await Task.WhenAll(tasks);
        
        return exchangeResponses;
    }
}