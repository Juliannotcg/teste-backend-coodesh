using Coodesh.Challenge.Pokemon.WebApi.Shared.Constants;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;

public static class ModelStateExtensions
{
    public static IEnumerable<ValidationFailure> GetErrorsAsValidationFailures(this ModelStateDictionary modelState)
        => modelState
            .Where(modelEntry => modelEntry.Value != null && modelEntry.Value.ValidationState == ModelValidationState.Invalid)
            .Select(modelEntry => ParseModelEntryWithErrorToValidationFailure(modelEntry.Key, modelEntry.Value!));

    private static ValidationFailure ParseModelEntryWithErrorToValidationFailure(string entryKey, ModelStateEntry entryValue)
    {
        var errorComposition = string.Join(Environment.NewLine, entryValue.Errors.Select(x => x.ErrorMessage));

        return new ValidationFailure(entryKey, errorComposition)
        {
            ErrorCode = ApiErrorType.InvalidValue
        };
    }
}
