using System.Data.Common;
using CriptoGateway.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Contrib.HttpClient;

namespace CryptoGateway.API.Tests.Configuration;

public class SetupTestWebAppFactory :  WebApplicationFactory<Program>
{
    public Mock<HttpMessageHandler> handlerMock { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            ConfigureHttpClient(services);

            ConfigureEfCore(services);
        });
    }
    
    private void ConfigureHttpClient(IServiceCollection services)
    {
        var httpClientFactoryDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(IHttpClientFactory));

        if (httpClientFactoryDescriptor is not null)
        {
            services.Remove(httpClientFactoryDescriptor);
        }

        var httpClientDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(HttpClient));

        if (httpClientDescriptor is not null)
        {
            services.Remove(httpClientDescriptor);
        }

        this.handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var clientFactoryMock = new Mock<IHttpClientFactory>();

        clientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(() =>
            {
                var client = handlerMock.CreateClient();
                return client;
            });

        services.AddSingleton<IHttpClientFactory>(clientFactoryMock.Object);
    }
    
    private void ConfigureEfCore(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<CryptoGatewayContext>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddSingleton<DbConnection>(container =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        });

        services.AddDbContext<CryptoGatewayContext>((container, options) =>
        {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });
    }
}