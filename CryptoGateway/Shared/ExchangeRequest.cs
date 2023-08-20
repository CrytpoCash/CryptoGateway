namespace CryptoGateway.Shared;

public abstract class ExchangeRequest
{
    protected ExchangeRequest(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }
}