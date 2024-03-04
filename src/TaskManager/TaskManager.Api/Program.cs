using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text.Json;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Services.Auth;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

#region Services
services.AddScoped<IAuthService, AuthService>();
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
#endregion

services.AddDbContext<TaskManagerDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("taskmanager_db")));

services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

services.AddHttpClient("Auth", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["AuthApi:URL"]!);
});

services.AddDistributedMemoryCache();
services.AddSession();
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(_ =>
{
    _.SupportNonNullableReferenceTypes();
    _.UseInlineDefinitionsForEnums();
    _.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API - V1",
        Version = "v1"
    });

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

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(_ =>
{
    var rsa = RSA.Create();
    var key = File.ReadAllText(configuration["SecurityOptions:PublicKeyFilePath"]!);
    rsa.FromXmlString(key);

    _.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["SecurityOptions:Issuer"],
        ValidAudience = builder.Configuration["SecurityOptions:Audience"],
        IssuerSigningKey = new RsaSecurityKey(rsa)
    };
});

services.AddAuthorization(_ =>
{
    _.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSession();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
