
namespace CryptoGateway.Infra.Adapter.Kucoin;

public record KucoinCryptoData(KucoinCryptoResponse Data);

public record KucoinCryptoResponse(string Symbol, decimal Last);
