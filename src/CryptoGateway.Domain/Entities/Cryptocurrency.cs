namespace CryptoGateway.Domain.Entities;

public sealed class Cryptocurrency
{
    private readonly List<CryptocurrencyExchangeItem> _exchangeItems;
    
    public Cryptocurrency()
    {
        Id = Guid.NewGuid();
        _exchangeItems = new List<CryptocurrencyExchangeItem>();
    }
    public Cryptocurrency(string name, string symbol): this()
    {
        Name = name;
        Symbol = symbol;
    }

    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    
    public IReadOnlyCollection<CryptocurrencyExchangeItem> ExchangeItems => _exchangeItems;
    public void AddExchangeItem(CryptocurrencyExchangeItem item)
    {
        _exchangeItems.Add(item);
    }
}