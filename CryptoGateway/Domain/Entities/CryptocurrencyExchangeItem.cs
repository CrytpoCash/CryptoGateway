namespace CryptoGateway.Domain.Entities;

public sealed class CryptocurrencyExchangeItem
{
    protected CryptocurrencyExchangeItem()
    {
        Id = Guid.NewGuid();
    }
    
    public CryptocurrencyExchangeItem(Guid exchangeId, string cryptoSymbolExternal): this()
    {
        ExchangeId = exchangeId;
        CryptoSymbolExternal = cryptoSymbolExternal;
    }

    public Guid Id { get; private  set; }
    public Guid CryptocurrencyId { get; private set; }
    public Guid ExchangeId { get; private set; }
    public string CryptoSymbolExternal { get; private set; }
    
    public Cryptocurrency Cryptocurrency { get; private set; }
    public Exchange Exchange { get; private set; }
}