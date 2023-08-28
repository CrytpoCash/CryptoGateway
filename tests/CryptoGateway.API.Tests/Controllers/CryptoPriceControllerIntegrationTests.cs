using System.Net;
using System.Net.Http.Json;
using CryptoGateway.API.Tests.Configuration;
using CryptoGateway.Core.Adapter;
using Moq;
using Moq.Protected;

namespace CryptoGateway.API.Tests.Controllers;

public class CryptoPriceControllerIntegrationTests : BaseIntegrationTest
{
    public CryptoPriceControllerIntegrationTests(SetupTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Get_Crypto_Price_Success()
    {
        var urls = new Dictionary<string, string>
        {
            { "https://api.binance.com/api/v3/ticker/24hr?symbol=BTCUSDT", "{\"symbol\":\"BTCUSDT\",\"askPrice\":\"1000.00\"}" },
            { "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT","{\"data\":{\"symbol\":\"BTC-USDT\",\"last\":\"1200.00\"}}" }
        };

        factory.handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                if (urls.TryGetValue(request.RequestUri.AbsoluteUri, out var jsonValue))
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(jsonValue)
                    };
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            });

        var response = await Client.GetFromJsonAsync<IEnumerable<ExchangeResponse>>("/CryptoPrice?symbol=BTC");

        Assert.Equal(2, response?.Count());
        Assert.Contains(response, x => x.Symbol == "BTCUSDT");
        Assert.Contains(response, x => x.Symbol == "BTC-USDT");
    }
}