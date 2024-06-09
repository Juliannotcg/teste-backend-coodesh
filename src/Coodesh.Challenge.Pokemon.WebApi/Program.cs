using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Middlewares;
using Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appSettings = AppSettingsBootStrapper.Register(builder);

var services = builder.Services;


builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(x
    => x.InvalidModelStateResponseFactory = ProblemDetailsMiddleware.HandleInvalidModelStateResponses);

services.AddApiVersionSetup();

services.AddSwagger();

services.AddHttpContextAccessor();

services.AddHttpClient();

services.AddMvc();

services.AddMemoryCache();

Configuration.RunServicesBootStrappers(builder, appSettings);
var app = builder.Build();

app.Services.GetService<ISqLiteDatabaseContext>();

Configuration.UseConfiguredServices(app);

app.MapControllers();

await app.RunAsync();

public partial class Program
{
    protected Program()
    {
    }
}
