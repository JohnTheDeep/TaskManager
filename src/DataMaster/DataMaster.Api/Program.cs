using DataMaster.Api.Auth;
using DataMaster.Api.Authentication;
using DataMaster.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddDbContext<DataMasterDbContext>(
    _ => _.UseNpgsql(configuration.GetConnectionString("postgres")));

services.AddControllers()
    .AddJsonOptions(_ =>
    {
        _.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        _.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

services.AddSwaggerGen(_ =>
{
    _.SupportNonNullableReferenceTypes();
    _.UseInlineDefinitionsForEnums();
    foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
        _.IncludeXmlComments(xmlFile);

    var securityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        }
    };

    _.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    _.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, Array.Empty<string>() }
        });
});

services.AddAuthentication("Bearer")
    .AddScheme<TokenSchemeOptions, TokenHandler>("Bearer", _ =>
    {
        _.AllowAnonymous = bool.Parse(configuration[$"Tokens:AllowAnonymous"]!);
        for (int i = 0 ; i < 10 ; i++)
            if (!string.IsNullOrEmpty(configuration[$"Tokens:{i}"]))
                _.ValidTokens.Add(configuration[$"Tokens:{i}"]!);
    });

services.AddAuthorization(_ =>
{
    _.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHealthChecks("/healthz");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
