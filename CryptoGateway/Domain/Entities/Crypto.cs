using System.Collections.ObjectModel;

namespace CryptoGateway.Domain.Entities;

public sealed class Crypto
{
    private readonly List<CryptoSymbolExchange> _cryptoSymbolExchanges = new();
    public Crypto(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public ReadOnlyCollection<CryptoSymbolExchange> CryptoSymbolExchanges { get => _cryptoSymbolExchanges.AsReadOnly(); }
    public void AddCryptoSymbolExchange(CryptoSymbolExchange item)
    {
        _cryptoSymbolExchanges.Add(item);
    }
}