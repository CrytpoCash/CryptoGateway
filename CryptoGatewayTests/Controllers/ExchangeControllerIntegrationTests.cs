using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CryptoGateway.ViewModels;
using CryptoGatewayTests.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CryptoGatewayTests.Controllers;

public class ExchangeControllerIntegrationTests : BaseIntegrationTest
{
    public ExchangeControllerIntegrationTests(SetupTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Add_New_Exchange_Success()
    {
        var model = new ExchangeInputViewModel("Exchange Test", "https://api.exchange-test.com");

        var response = await Client.PostAsJsonAsync("/exchange", model);

        var jsonStream  = await response.Content.ReadAsStreamAsync();
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var exchangeResultApi = await JsonSerializer.DeserializeAsync<ExchangeResultViewModel>(jsonStream, options);
        var exchangeFromDb = await CryptoGatewayContext.Exchanges.FirstOrDefaultAsync(x => x.Id == exchangeResultApi.Id);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(exchangeFromDb);
    }
}