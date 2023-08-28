namespace CryptoGateway.Core.Adapter;

public sealed class ExchangeResponse
{
    public ExchangeResponse(string symbol, string price, string exchangeName)
    {
        Symbol = symbol;
        Price = price;
        ExchangeName = exchangeName;
    }
    public string Symbol { get; set; }
    public string Price { get; set; }
    public string ExchangeName { get; set; }
}