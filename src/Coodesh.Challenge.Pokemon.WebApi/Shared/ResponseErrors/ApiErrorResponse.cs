using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;

public class ApiErrorResponse
{
    public ApiErrorResponse()
    {
        Type = string.Empty;
        Errors = new List<ApiErrorItemResponse>().AsReadOnly();
    }

    public ApiErrorResponse(string type, List<ApiErrorItemResponse> errors)
    {
        Type = type;
        Errors = errors is null ? new List<ApiErrorItemResponse>().AsReadOnly() : errors.AsReadOnly();
    }

    public string Type { get; init; }
    public IReadOnlyList<ApiErrorItemResponse> Errors { get; init; }

    public IActionResult AsResult(HttpStatusCode statusCode) => new ObjectResult(this)
    {
        StatusCode = (int)statusCode
    };
}
