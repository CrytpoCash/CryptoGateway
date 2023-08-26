using CryptoGateway.Infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoGatewayTests.Configuration;

public abstract class BaseIntegrationTest : IClassFixture<SetupTestWebAppFactory>
{
    protected readonly SetupTestWebAppFactory factory;
    protected readonly IServiceScope Scope;
    protected readonly HttpClient Client;
    protected readonly CryptoGatewayContext CryptoGatewayContext;

    public BaseIntegrationTest(SetupTestWebAppFactory factory)
    {
        this.factory = factory;
    
        Scope = factory.Services.CreateScope();
    
        CryptoGatewayContext = Scope.ServiceProvider.GetRequiredService<CryptoGatewayContext>();
    
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
}