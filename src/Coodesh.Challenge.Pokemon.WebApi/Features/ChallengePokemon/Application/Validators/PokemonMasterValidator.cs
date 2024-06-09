using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Constants;
using FluentValidation;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Validators;

public class PokemonMasterValidator : AbstractValidator<RegisterCapturePokemonViewModel>
{
    private readonly IApiPokemonExternalService _apiPokemonExternalService;
    private readonly IPokemonMasterRepository _pokemonMasterRepository;

    public PokemonMasterValidator(
        IApiPokemonExternalService apiPokemonExternalService,
        IPokemonMasterRepository pokemonMasterRepository)
    {
        _apiPokemonExternalService = apiPokemonExternalService;
        _pokemonMasterRepository = pokemonMasterRepository;
        RuleCapturePokemon();
    }

    protected void RuleCapturePokemon()
    {
        RuleFor(payload => payload.IdPokemonMaster)
             .NotEmpty()
             .WithErrorCode(ApiErrorType.MissingValue)
             .WithMessage("IdPokemonMaster master is required.");

        RuleFor(payload => payload.IdPokemon)
             .NotEmpty()
             .WithErrorCode(ApiErrorType.MissingValue)
             .WithMessage("IdPokemon is required.");

        RuleFor(x => x)
           .MustAsync((x, _, __, cancellationToken) => BePokemonMasterExits(x, cancellationToken))
           .WithMessage("The Pokemon master not found")
           .WithErrorCode(ApiErrorType.KeyNotFound);

        RuleFor(x => x)
           .MustAsync((x, _, __, cancellationToken) => BePokemonExits(x, cancellationToken))
           .WithMessage("The Pokemon with not found")
           .WithErrorCode(ApiErrorType.KeyNotFound);
    }

    private async Task<bool> BePokemonMasterExits(RegisterCapturePokemonViewModel registerCapturePokemonViewModel, CancellationToken cancellationToken)
    {
        var result = await _pokemonMasterRepository.PokemonMasterExistAsync(registerCapturePokemonViewModel.IdPokemonMaster, cancellationToken);
        return result;
    }

    private async Task<bool> BePokemonExits(RegisterCapturePokemonViewModel registerCapturePokemonViewModel, CancellationToken cancellationToken)
    {
        var result = await _apiPokemonExternalService.GetApiPokemonsByIdAsync(registerCapturePokemonViewModel.IdPokemon, cancellationToken);
        return result != null;
    }



}
