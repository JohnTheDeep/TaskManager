using MediatR;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.User;

namespace TaskManager.Core.DTOs.Requests.User;

public class LoginUserReqeust : IRequest<BaseResponseDto<LoginUserResponse>>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
