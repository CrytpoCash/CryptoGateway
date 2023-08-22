namespace CryptoGateway.Shared;

public sealed class ExchangeResponse
{
    public ExchangeResponse(string symbol, decimal price)
    {
        Symbol = symbol;
        Price = price;
    }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
}