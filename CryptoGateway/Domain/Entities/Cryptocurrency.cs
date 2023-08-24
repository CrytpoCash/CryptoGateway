namespace CryptoGateway.Domain.Entities;

public sealed class Cryptocurrency
{
    private readonly List<CryptoSymbolExchange> _cryptoSymbolExchanges;
    
    public Cryptocurrency()
    {
        Id = Guid.NewGuid();
        _cryptoSymbolExchanges = new List<CryptoSymbolExchange>();
    }
    public Cryptocurrency(string name, string symbol): this()
    {
        Name = name;
        Symbol = symbol;
    }

    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    
    public IReadOnlyCollection<CryptoSymbolExchange> CryptoSymbolExchanges => _cryptoSymbolExchanges;
    public void AddCryptoSymbolExchange(CryptoSymbolExchange item)
    {
        _cryptoSymbolExchanges.Add(item);
    }
}