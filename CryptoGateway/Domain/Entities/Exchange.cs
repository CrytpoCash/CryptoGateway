namespace CryptoGateway.Domain.Entities;

public sealed class Exchange
{
    public Exchange(string name, string baseUrl)
    {
        Name = name;
        BaseURL = baseUrl;
    }
    public string Name { get; private set; }
    public string BaseURL  { get; private set; }
}