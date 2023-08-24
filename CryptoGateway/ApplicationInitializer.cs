using CryptoGateway.Infra;

namespace CryptoGateway;

public static class ApplicationInitializer
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetService<CryptoGatewayContext>();
        context?.Database.EnsureCreated();
    }
}