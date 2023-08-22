namespace CryptoGateway.Domain.Entities;

public sealed class CryptoSymbolExchange
{
    public CryptoSymbolExchange(Exchange exchange, string cryptoSymbol)
    {
        Exchange = exchange;
        CryptoSymbol = cryptoSymbol;
    }
    
    public Exchange Exchange { get; private set; }
    public string CryptoSymbol { get; private set; }
}