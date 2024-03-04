namespace TaskManager.Core.DTOs.Responses.User;

public record UpdateProfileResponse : BaseResponse
{
    public Models.User UpdatedUser { get; set; }
}
