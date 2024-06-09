using FluentValidation;
using FluentValidation.Results;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors.Interfaces;

public interface IApiErrorBuilder
{
    public ApiErrorResponse BuildBadRequest(HttpRequest request);

    public ApiErrorResponse BuildBadRequest(HttpRequest request, IEnumerable<ValidationFailure> validationFailures);

    public ApiErrorResponse BuildBadRequest(HttpRequest request, ValidationException validationException);

    public ApiErrorResponse BuildInternalError(HttpRequest request);

    public ApiErrorResponse BuildKeyNotFound(HttpRequest request);
}
