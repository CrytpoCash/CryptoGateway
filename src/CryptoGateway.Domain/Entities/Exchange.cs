namespace CryptoGateway.Domain.Entities;

public sealed class Exchange
{
    protected Exchange()
    {
        Id = Guid.NewGuid();
    }
    public Exchange(string name, string baseUrl) : this()
    {
        Name = name;
        BaseURL = baseUrl;
    }

    public Guid Id { get; private set; } 
    public string Name { get; set; }
    public string BaseURL  { get; set; }
}