namespace CryptoGateway.ViewModels;

public class ExchangeInputViewModel
{
    public string Name { get; set; }
    public string BaseURL { get; set; }
}

public record ExchangeResultViewModel (Guid Id);