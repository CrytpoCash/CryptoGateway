using CriptoGateway.Infra.Factories;
using CryptoGateway.Core.Adapter;
using CryptoGateway.Domain.Contracts;
using CryptoGateway.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CryptoGateway.Application.Services;

public class CryptoPriceService : ICryptoPriceService
{
    private readonly ILogger<CryptoPriceService> _logger;
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IExchangeApiAdapterFactory _exchangeApiAdapterFactory;

    public CryptoPriceService(
        ICryptocurrencyRepository cryptocurrencyRepository, 
        IExchangeApiAdapterFactory exchangeApiAdapterFactory, ILogger<CryptoPriceService> logger)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _exchangeApiAdapterFactory = exchangeApiAdapterFactory;
        _logger = logger;
    }

    public async Task<IEnumerable<ExchangeResponse>> GetCryptoPriceAsync(string symbol)
    {
        var cryptocurrency = await _cryptocurrencyRepository.GetBySymbolAsync(symbol);

        if (cryptocurrency is null || !cryptocurrency.ExchangeItems.Any()) 
        {
            return Enumerable.Empty<ExchangeResponse>();
        }

        var priceFetchTasks = cryptocurrency.ExchangeItems
            .Select(CreatePriceFetchTask)
            .ToList();

        await SafeWhenAll(priceFetchTasks);

        return FilterSuccessfulResults(priceFetchTasks);
    }

    private Task<ExchangeResponse> CreatePriceFetchTask(CryptocurrencyExchangeItem item)
    {
        var adapter = _exchangeApiAdapterFactory.CreateAdapter(item.Exchange.BaseURL);
        return adapter.GetCryptoPriceAsync(item.CryptoSymbolExternal);
    }

    private async Task SafeWhenAll(IEnumerable<Task<ExchangeResponse>> tasks)
    {
        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching prices: {ex.Message}");
        }
    }

    private IEnumerable<ExchangeResponse> FilterSuccessfulResults(IEnumerable<Task<ExchangeResponse>> tasks)
    {
        return tasks
            .Where(task => task.Status != TaskStatus.Faulted)
            .Select(task => task.Result);
    }
}