using System.Diagnostics.CodeAnalysis;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;


public static class ConfigurationExtensions
{
    public static string GetAppKey(this IConfiguration configuration, string key)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Error fetching application variable, parameter cannot be null...", key);
        }

        return value;
    }
}