namespace CryptoGateway.Shared;

public sealed class ExchangeResponse
{
    public ExchangeResponse(string name, string symbol, decimal price)
    {
        Name = name;
        Symbol = symbol;
        Price = price;
    }
    public string Name { get; private set; }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
}