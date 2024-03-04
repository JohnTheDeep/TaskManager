namespace Auth.Core.Responses;

public record RegisterResponse : BaseResponse
{
    public bool IsRegistered { get; set; }
}
