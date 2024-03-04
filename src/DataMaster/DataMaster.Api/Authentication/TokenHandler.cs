using DataMaster.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace DataMaster.Api.Auth
{
    public class TokenHandler : AuthenticationHandler<TokenSchemeOptions>
    {
        public TokenHandler(
            IOptionsMonitor<TokenSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder, new SystemClock())
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers[HeaderNames.Authorization].ToString();
            if (string.IsNullOrWhiteSpace(authorization))
            {
                if (Options.AllowAnonymous)
                {
                    Logger.LogWarning($"Anonymous access from: {Context.Connection.RemoteIpAddress}");
                    return Task.FromResult(AuthenticateResult.Success(CreateTicket("anonymous")));
                }

                return Task.FromResult(AuthenticateResult.Fail($"Empty token provided by: {Context.Connection.RemoteIpAddress}"));
            }

            if (authorization.StartsWith("Bearer "))
            {
                authorization = authorization.Substring("Bearer ".Length);

                if (Options.ValidTokens.Contains(authorization))
                {
                    var name = authorization.Split("_")[0];
                    Logger.LogInformation($"Authentication successfull: {name}");
                    return Task.FromResult(AuthenticateResult.Success(CreateTicket(name)));
                }
            }

            return Task.FromResult(AuthenticateResult.Fail($"Incorrect token provided by: {Context.Connection.RemoteIpAddress}"));
        }

        private AuthenticationTicket CreateTicket(string name)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) }, Scheme.Name);
            return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
        }
    }
}
