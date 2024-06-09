using System.Diagnostics.CodeAnalysis;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;


public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.MapType<Exception>(() => new OpenApiSchema { Type = "object" });

            c.EnableAnnotations();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });

        services.ConfigureOptions<ConfigureSwaggerApiVersions>();
    }

    public static void UseSwaggerDocumentation(this WebApplication app)
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var groupNames = apiVersionDescriptionProvider.ApiVersionDescriptions.Select(x => x.GroupName);

            foreach (var group in groupNames)
            {
                options.SwaggerEndpoint($"/swagger/{group}/swagger.json", group.ToUpperInvariant());
            }
        });
    }
}