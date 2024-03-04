using MediatR;
using TaskManager.Core.DTOs.Responses.User;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.User;

public record UpdateProfileRequest : IRequest<BaseResponseDto<UpdateProfileResponse>>
{
    public string? NewName { get; set; }
    public string? NewEmail { get; set; }
    public string UserId { get; set; }
}
