using MediatR;
using TaskManager.Core.DTOs.Requests.User;
using TaskManager.Core.DTOs.Responses.User;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.UserUseCases
{
    public class UpdateProfileUseCase : IRequestHandler<UpdateProfileRequest, BaseResponseDto<UpdateProfileResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<User> _userRepository;

        public UpdateProfileUseCase(IMediator mediator, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDto<UpdateProfileResponse>> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Guid.TryParse(request.UserId, out Guid userGuid);

                var user = (await _userRepository.GetByGuidAsync(userGuid));

                if (user is null) return new BaseResponseDto<UpdateProfileResponse>
                {
                    Errors = new string[] { "Client was not founded!" },
                    Data = new UpdateProfileResponse
                    {
                        StatusCode = 500
                    }
                };

                user.Email = !string.IsNullOrEmpty(request.NewEmail) ? request.NewEmail : user.Email;
                user.Name = !string.IsNullOrEmpty(request.NewName) ? request.NewName : user.Name;

                await _userRepository.UpdateAsync(user);

                return new BaseResponseDto<UpdateProfileResponse>
                {
                    Data = new UpdateProfileResponse
                    {
                        StatusCode = 200,
                        UpdatedUser = user
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed update profile", ex);
            }
        }
    }
}
