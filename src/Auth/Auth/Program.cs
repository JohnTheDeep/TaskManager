using Auth.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AccountsDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("accounts_db")));
builder.Services.AddDbContext<TaskManagerDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("taskmanager_db")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var rsa = RSA.Create();
                    string key = File.ReadAllText(builder.Configuration["SecurityOptions:PublicKeyFilePath"]!);
                    rsa.FromXmlString(key);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["SecurityOptions:Issuer"],
                        ValidAudience = builder.Configuration["SecurityOptions:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(rsa),
                    };
                });

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
