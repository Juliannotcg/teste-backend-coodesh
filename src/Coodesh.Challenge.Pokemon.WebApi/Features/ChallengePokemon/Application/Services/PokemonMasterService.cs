using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Response;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;
using Coodesh.Challenge.Pokemon.WebApi.Features.MetricsCollector.Domain.Models;
using FluentValidation;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Services;

public class PokemonMasterService : IPokemonMasterService
{
    public readonly IPokemonMasterRepository _pokemonMasterRepository;
    public readonly IPokemonRepository _pokemonRepository;

    public readonly IApiPokemonExternalService _apiPokemonExternalService;
    private readonly IValidator<RegisterCapturePokemonViewModel> _createValidator;

    private readonly ILogger<PokemonService> _logger;

    public PokemonMasterService(
        ILogger<PokemonService> logger,
        IPokemonMasterRepository pokemonMasterRepository,
        IApiPokemonExternalService apiPokemonExternalService,
        IValidator<RegisterCapturePokemonViewModel> createValidator,
        IPokemonRepository pokemonRepository)
    {
        _logger = logger;
        _pokemonMasterRepository = pokemonMasterRepository;
        _apiPokemonExternalService = apiPokemonExternalService;
        _createValidator = createValidator;
        _pokemonRepository = pokemonRepository;
    }

    public async Task AddPokemonMasterAsync(PokemonMasterViewModel pokemonMaster, CancellationToken cancellationToken)
    {
        var pokemonMasterModel = new PokemonMasterModel(pokemonMaster.Name, pokemonMaster.Age, pokemonMaster.CPF);

        await _pokemonMasterRepository.AddPokemonMasterAsync(pokemonMasterModel, cancellationToken);
    }

    public async Task RegisterCapturePokemonAsync(RegisterCapturePokemonViewModel capturePokemon, CancellationToken cancellationToken)
    {
        await _createValidator.ValidateAndThrowAsync(capturePokemon, cancellationToken);

        var pokemon = await _apiPokemonExternalService.GetApiPokemonsByIdAsync(capturePokemon.IdPokemon, cancellationToken);

        await _pokemonRepository.AddPokemonAsync(
            new PokemonModel(pokemon!.id, pokemon.name, pokemon.sprites.front_default, pokemon.sprites.back_default, pokemon.base_experience),
            cancellationToken);

        await _pokemonMasterRepository.AddCapturePokemonAsync(capturePokemon.IdPokemonMaster, capturePokemon.IdPokemon, cancellationToken);
    }

    public async Task<IEnumerable<PokemonCapturedViewModel>> GetCapturePokemonAsync(int idPokemonMaster, CancellationToken cancellationToken)
    {
        var result = await _pokemonMasterRepository.GetPokemonsByIdPokemonMasterAsync(idPokemonMaster, cancellationToken);
        return result.Select(x => new PokemonCapturedViewModel(x.Name, x.ImageFront, x.ImageBack, x.Experience));
    }
}
