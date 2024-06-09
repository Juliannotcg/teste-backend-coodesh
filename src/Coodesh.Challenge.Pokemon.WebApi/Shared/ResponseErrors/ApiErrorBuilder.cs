using Coodesh.Challenge.Pokemon.WebApi.Shared.Constants;
using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;

public class ApiErrorBuilder : IApiErrorBuilder
{
    public ApiErrorResponse BuildBadRequest(HttpRequest request) => Build(
            request,
            ApiErrorItemResponse.Build(ApiErrorType.BadRequest, "Bad Request", "Bad request error")
        );

    public ApiErrorResponse BuildBadRequest(HttpRequest request, IEnumerable<ValidationFailure> validationFailures)
    {
        if (validationFailures.Count() is 0)
        {
            return BuildBadRequest(request);
        }

        return Build(
            request,
            ApiErrorItemResponse.Build(ApiErrorType.BadRequest, "Bad Request", validationFailures)
        );
    }

    public ApiErrorResponse BuildBadRequest(HttpRequest request, ValidationException validationException)
    {
        if (validationException.Errors.Count() is 0)
        {
            return BuildBadRequest(request);
        }

        return Build(
            request,
            ApiErrorItemResponse.Build(ApiErrorType.BadRequest, "Bad Request", validationException)
        );
    }

    public ApiErrorResponse BuildInternalError(HttpRequest request) => Build(
            request,
            ApiErrorItemResponse.Build(ApiErrorType.InternalServerError, "Internal server error", "Internal server error")
        );

    public ApiErrorResponse BuildKeyNotFound(HttpRequest request) => Build(
            request,
            ApiErrorItemResponse.Build(ApiErrorType.KeyNotFound, "Key not found", "Key not found")
        );

    public static ApiErrorResponse Build(HttpRequest request, string type, string title, string description) => Build(
            request,
            ApiErrorItemResponse.Build(type, title, description)
        );

    private static ApiErrorResponse Build(HttpRequest request, List<ApiErrorItemResponse> errorItems) => new(
            request.Path,
            errorItems
        );
}
