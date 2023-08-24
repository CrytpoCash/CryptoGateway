namespace CryptoGateway.ViewModels;

public class CryptocurrencyInputViewModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public IEnumerable<ExchangeItemViewModel> Exchanges { get; set; }
    
    public class ExchangeItemViewModel
    {
        public Guid ExchangeId { get; set; }
        public string Symbol { get; set; }
    }
}