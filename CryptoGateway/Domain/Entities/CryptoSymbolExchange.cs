namespace CryptoGateway.Domain.Entities;

public sealed class CryptoSymbolExchange
{
    protected CryptoSymbolExchange()
    {
        Id = Guid.NewGuid();
    }
    
    public CryptoSymbolExchange(Exchange exchange, string cryptoSymbol): this()
    {
        Exchange = exchange;
        CryptoSymbol = cryptoSymbol;
    }

    public Guid Id { get; private  set; }
    public Exchange Exchange { get; private set; }
    public string CryptoSymbol { get; private set; }
}