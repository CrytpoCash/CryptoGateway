using Polly;
using Polly.Extensions.Http;

namespace CryptoGateway.API.Extensions;

public static class PollyExtensions
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var retryCount = 3;
        
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(
                retryCount, 
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (_, _, retryAttempt, context) =>
            {
                Console.WriteLine($"Retry attempt ({retryAttempt} of {retryCount})");
            });
    }
}