namespace Auth.Core.Responses;

public abstract record BaseResponse
{
    public int StatusCode { get; set; }
    public string? Response { get; set; }
}
