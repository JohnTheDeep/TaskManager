using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class CreateTaskListHandler : IRequestHandler<CreateTaskListRequest, BaseResponseDto<CreateTasksListResponse>>
    {
        private readonly IRepository<TasksList> _repository;
        private readonly IRepository<User> _userRepository;

        public CreateTaskListHandler(IRepository<TasksList> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDto<CreateTasksListResponse>> Handle(CreateTaskListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new BaseResponseDto<CreateTasksListResponse>();

                if (string.IsNullOrEmpty(request.UserId)) return
                    new BaseResponseDto<CreateTasksListResponse>
                    {
                        Errors = new string[] { "Invalid UserId" },
                        Data = new CreateTasksListResponse
                        {
                            StatusCode = 500
                        }
                    };

                var user = await _userRepository.GetByGuidAsync(Guid.Parse(request.UserId));

                return new BaseResponseDto<CreateTasksListResponse>
                {
                    Data = new CreateTasksListResponse
                    {
                        Guid = await _repository.CreateAsync(new TasksList
                        {
                            CreatedAt = DateTime.UtcNow,
                            Description = request.Description,
                            Name = request.Name,
                            User = user
                        }),
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed get tasks list", ex);
            }
        }
    }
}
