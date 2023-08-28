namespace CryptoGateway.API.ViewModels;

public record CryptocurrencyInputViewModel(string Name, string Symbol, IEnumerable<ExchangeItemViewModel> Exchanges);
public record ExchangeItemViewModel(Guid ExchangeId, string Symbol);