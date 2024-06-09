using Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;
using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;
using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Middlewares;


public static class ProblemDetailsMiddleware
{
    public static async Task HandleUnhandledExceptions(HttpContext context)
    {
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandler is null)
        {
            return;
        }

        Exception givenException = exceptionHandler.Error;

        var errorBuilder = context.RequestServices.GetRequiredService<IApiErrorBuilder>();

        var errorResponse = BuildExceptionResponse(errorBuilder, context.Request, givenException).ToJson();
        var errorStatusCode = GetResponseStatusCodeFromException(givenException);

        LogHandledException(context, errorStatusCode, errorResponse, givenException);

        context.Response.StatusCode = errorStatusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsync(errorResponse);
    }

    private static ApiErrorResponse BuildExceptionResponse(IApiErrorBuilder errorBuilder, HttpRequest request, Exception exception) => exception switch
    {
        BadHttpRequestException => errorBuilder.BuildBadRequest(request),
        ValidationException validationException => errorBuilder.BuildBadRequest(request, validationException),
        KeyNotFoundException => errorBuilder.BuildKeyNotFound(request),
        _ => errorBuilder.BuildInternalError(request)
    };

    private static int GetResponseStatusCodeFromException(Exception exception)
        => exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

    private static void LogHandledException(HttpContext context, int statusCode, string response, Exception exception)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        const string LogMessage = "HTTP Status: {statusCode} - Response given: {response} - Details: {Message}";

        if (statusCode is StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, LogMessage, statusCode, response, exception.Message);
        }
        else
        {
            logger.LogWarning(exception, LogMessage, statusCode, response, exception.Message);
        }
    }

    public static IActionResult HandleInvalidModelStateResponses(ActionContext actionContext)
    {
        var logger = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        var errorBuilder = actionContext.HttpContext.RequestServices.GetRequiredService<IApiErrorBuilder>();

        var badRequestResponse = errorBuilder.BuildBadRequest(actionContext.HttpContext.Request, actionContext.ModelState.GetErrorsAsValidationFailures());

        const string LogMessage = "HTTP Status: 400 - Response given: {Response}";

        logger.LogWarning(LogMessage, badRequestResponse.ToJson());

        return new BadRequestObjectResult(badRequestResponse)
        {
            ContentTypes = { "application/problem+json" }
        };
    }
}

