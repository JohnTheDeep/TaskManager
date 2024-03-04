using Auth.Core.Responses;

namespace Auth.Core.Responsesl;

public record LoginResponse : BaseResponse
{
    public string? Token { get; set; }
    public string? UserId { get; set; }
}
