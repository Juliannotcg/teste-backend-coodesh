using System.Net;
using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Controllers;

[ApiController]
[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Invalid sent data", Type = typeof(ApiErrorResponse), ContentTypes = new[] { "application/json" })]
[SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Unauthenticated user", Type = typeof(ApiErrorResponse), ContentTypes = new[] { "application/json" })]
[SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "User without authorization to access the resource", Type = typeof(ApiErrorResponse), ContentTypes = new[] { "application/json" })]
[SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Internal server error", Type = typeof(ApiErrorResponse), ContentTypes = new[] { "application/json" })]
public abstract class CustomControllerBase : ControllerBase { }
