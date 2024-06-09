using System.Net;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Services;
using Polly;
using Polly.Extensions.Http;
using Serilog;


namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;

public static class RequestServiceExtensions
{
    private static readonly TimeSpan[] DefaultRetries =
  [
      TimeSpan.FromSeconds(10),
  ];

    private static readonly HttpStatusCode[] RetriableHttpStatusCode =
    [
        HttpStatusCode.BadGateway,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout
    ];

    public static void AddRequestService<TImplementation>(
        this IServiceCollection services,
        string baseUrlAddress
    )
        where TImplementation : class => services
            .AddHttpClient(
                typeof(TImplementation).Name,
                client =>
                {
                    client.BaseAddress = new Uri(baseUrlAddress);
                }
            )
            .AddPolicyHandler(RetryPolicy)
            .AddHttpMessageHandler<RequestServiceLogger>();

    private static IAsyncPolicy<HttpResponseMessage> RetryPolicy
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => RetriableHttpStatusCode.Contains(response.StatusCode))
            .WaitAndRetryAsync(
                DefaultRetries,
                (result, timeSpan, retryCount, Context) =>
                {
                    const string LogMessage = "REQUEST RETRY ERROR\nUri:{Uri}\nHTTP Status Code: {StatusCode}\nRetry Count: {CurrentRetryCount}\nCause: {ExceptionMessage}";

                    Log.Error(
                        LogMessage,
                        result.Result?.RequestMessage?.RequestUri?.AbsoluteUri,
                        result.Result?.StatusCode,
                        retryCount,
                        result?.Exception?.Message ?? "-"
                    );
                }
            );
}
