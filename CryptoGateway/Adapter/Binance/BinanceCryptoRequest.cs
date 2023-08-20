using CryptoGateway.Shared;

namespace CryptoGateway.Adapter.Binance;

public sealed class BinanceCryptoRequest : ExchangeRequest
{
    public BinanceCryptoRequest(string name, string cryptoSymbol) : base(name)
    {
        CryptoSymbol = cryptoSymbol;
    }

    public string CryptoSymbol { get; private set; }
}