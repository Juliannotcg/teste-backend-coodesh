using FluentValidation;
using FluentValidation.Results;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;

public class ApiErrorItemResponse
{
    public ApiErrorItemResponse()
    {
        Type = string.Empty;
        Error = string.Empty;
        Detail = string.Empty;
    }

    private ApiErrorItemResponse(string type, string error, string detail)
    {
        Type = type;
        Error = error;
        Detail = detail;
    }

    public string Type { get; init; }
    public string Error { get; init; }
    public string Detail { get; init; }

    public static List<ApiErrorItemResponse> Build(string type, string title, string message)
        => new() { new ApiErrorItemResponse(type, title, message) };

    public static List<ApiErrorItemResponse> Build(string type, string title, ValidationException exception)
        => ParseValidationFailuresToErrorItems(type, title, exception.Errors);

    public static List<ApiErrorItemResponse> Build(string type, string title, IEnumerable<ValidationFailure> validationFailures)
        => ParseValidationFailuresToErrorItems(type, title, validationFailures);

    private static List<ApiErrorItemResponse> ParseValidationFailuresToErrorItems(string type, string title, IEnumerable<ValidationFailure> validationFailures)
        => validationFailures.Select(
            x => new ApiErrorItemResponse(type, title, x.ErrorMessage)
        ).ToList();
}
