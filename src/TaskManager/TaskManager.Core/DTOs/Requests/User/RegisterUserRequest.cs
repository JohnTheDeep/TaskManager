using MediatR;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.User;

namespace TaskManager.Core.DTOs.Requests.User;

public class RegisterUserRequest : IRequest<BaseResponseDto<RegisterUserResponse>>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
