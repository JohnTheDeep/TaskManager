using Microsoft.AspNetCore.Authentication;

namespace DataMaster.Api.Authentication;

public class TokenSchemeOptions : AuthenticationSchemeOptions
{
    public bool AllowAnonymous { get; set; }

    public List<string> ValidTokens { get; set; } = new();
}