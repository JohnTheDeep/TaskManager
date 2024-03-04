using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{

    public class AttachTaskToListHandler : IRequestHandler<AttachTaskToListRequest, BaseResponseDto<AttachTaskToListResponse>>
    {
        private readonly IRepository<Models.Task> _repository;
        private readonly IRepository<TasksList> _tasksListRepository;

        public AttachTaskToListHandler(IRepository<Models.Task> repository, IRepository<TasksList> tasksListRepository)
        {
            _repository = repository;
            _tasksListRepository = tasksListRepository;
        }

        public async Task<BaseResponseDto<AttachTaskToListResponse>> Handle(AttachTaskToListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await _tasksListRepository.GetByGuidAsync(request.TaskListId);

                return new BaseResponseDto<AttachTaskToListResponse>
                {
                    Data = new AttachTaskToListResponse
                    {
                        id = await _repository.CreateAsync(new Models.Task
                        {
                            CreatedAt = DateTime.UtcNow,
                            Description = request.Description,
                            Name = request.Name,
                            State = Enums.TaskState.AtPending,
                            List = taskList
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
