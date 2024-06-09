using System.Net;
using System.Text.Json;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Services;

public class RequestService<TClass> : IRequestService<TClass> where TClass : class
{
    private readonly HttpClient _httpClient;

    public RequestService(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient(typeof(TClass).Name);

    public async Task<TResponse?> GetAsync<TResponse>(
        string endpoint,
        IDictionary<string, string?>? queryParams,
        CancellationToken cancellationToken)
    {
        var response = await SendAsync(HttpMethod.Get, endpoint, queryParams, null, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return default;
        }

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<TResponse?>(body);
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, IDictionary<string, string?>? queryParams, HttpContent? content, CancellationToken cancellationToken)
    {
        if (queryParams is not null)
        {
            url = QueryHelpers.AddQueryString(url, queryParams);
        }

        using var requestMessage = new HttpRequestMessage();

        requestMessage.Method = method;
        requestMessage.RequestUri = new Uri(_httpClient.BaseAddress + url);

        if (content is not null)
        {
            requestMessage.Content = content;
        }

        return await _httpClient.SendAsync(requestMessage, cancellationToken);
    }
}
