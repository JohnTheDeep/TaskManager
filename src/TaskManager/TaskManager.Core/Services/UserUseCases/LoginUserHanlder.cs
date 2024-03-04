using MediatR;
using TaskManager.Core.DTOs.Requests.User;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.User;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Services.UserUseCases
{
    public class LoginUserHanlder : IRequestHandler<LoginUserReqeust, BaseResponseDto<LoginUserResponse>>
    {
        private readonly IAuthService _authService;

        public LoginUserHanlder(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<BaseResponseDto<LoginUserResponse>> Handle(LoginUserReqeust request, CancellationToken cancellationToken)
        {
            try
            {
                var loginResponse = await _authService.LoginAsync(request.Login, request.Password);

                if (loginResponse.StatusCode != 200)
                    return new BaseResponseDto<LoginUserResponse>
                    {
                        Errors = new string[] { "Invalid credentials" },
                        Data = loginResponse
                    };

                return new BaseResponseDto<LoginUserResponse>
                {
                    Data = loginResponse
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed login", ex);
            }
        }
    }
}
