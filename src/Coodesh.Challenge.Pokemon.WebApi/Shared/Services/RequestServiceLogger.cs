using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Services;


public class RequestServiceLogger : DelegatingHandler
{
    private static readonly string[] StopWords = [];
    private static readonly string[] StopWordsConcat = [];

    private readonly ILogger<RequestServiceLogger> _logger;

    public RequestServiceLogger(
        ILogger<RequestServiceLogger> logger
    ) => _logger = logger;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        await LogRequest(request, response);

        return response;
    }

    private async Task LogRequest(HttpRequestMessage request, HttpResponseMessage response)
    {
        string requestBody = string.Empty;
        string responseBody = string.Empty;

        if (request?.Content is not null)
        {
            requestBody = RemoveSensitiveData(await request.Content.ReadAsStringAsync());
        }

        if (response?.Content is not null)
        {
            responseBody = RemoveSensitiveData(await GetResponse(response));
        }

        const string LogMessage = "REQUEST COMPLETED\nUri: {Uri}\nHTTP Method: {Method}\nHTTP Status Code: {StatusCode}\nRequest Body: {RequestBody}\nResponse Body: {ResponseBody}";

        _logger.LogInformation(
            LogMessage,
            RemoveSensitiveData(request?.RequestUri?.AbsoluteUri),
            request?.Method?.Method,
            response?.StatusCode,
            requestBody,
            responseBody
        );
    }

    private static async Task<string> GetResponse(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        try
        {
            return JToken.Parse(responseContent).ToString(Formatting.Indented);
        }
        catch
        {
            return responseContent;
        }
    }

    private static string RemoveSensitiveData(string? content)
    {
        if (content is null)
        {
            return string.Empty;
        }


        foreach (var word in StopWords)
        {
            var pattern = $"(\"{word}[^\"]*\"[^:]*:[^:\"]*\"[^\"]*\")";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            content = regex.Replace(content, $"\"{word}\": \"###\"");
        }

        foreach (var word in StopWordsConcat)
        {
            var pattern = $"{word}=[^(&|\")]*";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            content = regex.Replace(content, $"{word}=###");
        }

        return content;
    }
}
