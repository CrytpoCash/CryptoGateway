using CryptoGateway.Shared;

namespace CryptoGateway.Adapter.Kucoin;

public sealed class KucoinCryptoRequest : ExchangeRequest
{
    public KucoinCryptoRequest(string name, string cryptoSymbol) : base(name)
    {
        CryptoSymbol = cryptoSymbol;
    }

    public string CryptoSymbol { get; private set; }
}