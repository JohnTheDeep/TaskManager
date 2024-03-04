using MediatR;
using TaskManager.Core.DTOs.Requests.User;
using TaskManager.Core.DTOs.Responses.User;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Services.UserUseCases
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, BaseResponseDto<RegisterUserResponse>>
    {
        private readonly IAuthService _authService;

        public RegisterUserHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<BaseResponseDto<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registerResponse = await _authService.RegisterAsync(request);

                if (registerResponse.StatusCode != 200)
                    return new BaseResponseDto<RegisterUserResponse>
                    {
                        Errors = new string[] { registerResponse.Response ?? string.Empty },
                        Data = registerResponse
                    };

                return new BaseResponseDto<RegisterUserResponse>
                {
                    Data = registerResponse,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed register", ex);
            }
        }
    }
}
