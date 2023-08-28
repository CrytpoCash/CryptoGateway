
namespace CryptoGateway.Infra.Adapters.Kucoin;

public record KucoinCryptoData(KucoinCryptoResponse Data);

public record KucoinCryptoResponse(string Symbol, decimal Last);
