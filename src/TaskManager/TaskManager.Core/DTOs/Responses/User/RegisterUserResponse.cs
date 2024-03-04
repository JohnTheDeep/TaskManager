namespace TaskManager.Core.DTOs.Responses.User;

public record RegisterUserResponse : BaseResponse
{
    public string? Response { get; set; }
    public bool IsRegistered { get; set; }
}
