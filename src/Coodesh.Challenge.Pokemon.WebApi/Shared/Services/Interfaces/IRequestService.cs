namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Services.Interfaces;

public interface IRequestService<TClass> where TClass : class
{
    Task<TResponse?> GetAsync<TResponse>(
        string endpoint,
        IDictionary<string, string?>? queryParams,
        CancellationToken cancellationToken);
}
