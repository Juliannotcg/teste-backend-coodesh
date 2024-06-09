using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;

public static class JsonExtensions
{
    public static JsonSerializerOptions GetDefaultJsonSerializerOptions()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        return jsonSerializerOptions;
    }

    public static readonly JsonSerializerOptions JsonSerializerOptions = GetDefaultJsonSerializerOptions();
    public static T? FromJson<T>(this BinaryData message, JsonSerializerOptions? options = null)
    {
        var body = Encoding.UTF8.GetString(message);
        return body.FromJson<T>(options ?? JsonSerializerOptions);
    }

    public static T? FromJson<T>(this byte[]? message, JsonSerializerOptions? options = null)
    {
        if (message == null)
        {
            return default;
        }

        var body = Encoding.UTF8.GetString(message);
        return body.FromJson<T>(options ?? JsonSerializerOptions);
    }

    public static T? FromJson<T>(this Stream stream, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(stream, options ?? JsonSerializerOptions);

    public static T? FromJson<T>(this string json, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(json, options ?? JsonSerializerOptions);

    public static string ToJson<T>(this T obj, JsonSerializerOptions? options = null) => JsonSerializer.Serialize(obj, options ?? JsonSerializerOptions);
}
