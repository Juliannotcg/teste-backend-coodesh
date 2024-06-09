using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Services;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Validators;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;
using FluentValidation;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class ChallengePokemonServiceBootStrapper
{
    public static IServiceCollection InstallChallengePokemonService(this IServiceCollection services, IAppSettings appSettings)
    {
        services.AddScoped<IApiPokemonExternalService, ApiPokemonExternalService>();
        services.AddScoped<IPokemonService, PokemonService>();
        services.AddScoped<IPokemonMasterService, PokemonMasterService>();

        services.AddRequestService<ApiPokemonExternalService>(appSettings.ApiPokemonUrlBase);

        services.AddScoped<IPokemonMasterRepository, PokemonMasterRepository>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();

        services.AddScoped<IValidator<RegisterCapturePokemonViewModel>, PokemonMasterValidator>();
        return services;
    }
}
