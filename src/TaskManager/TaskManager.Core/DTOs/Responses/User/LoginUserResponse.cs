namespace TaskManager.Core.DTOs.Responses.User;

public record LoginUserResponse : BaseResponse
{
    public string? Token { get; set; }
    public string? UserId { get; set; }
}
