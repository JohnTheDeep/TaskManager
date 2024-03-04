using TaskManager.Core.DTOs.Requests.User;
using TaskManager.Core.DTOs.Responses.User;

namespace TaskManager.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginUserResponse> LoginAsync(string login, string password);
        Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request);
    }
}
