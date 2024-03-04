using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class UpdateTaskListHandler : IRequestHandler<UpdateTaskListRequest, BaseResponseDto<UpdateTaskListResponse>>
    {
        private readonly IMediator _mediatr;
        private readonly IRepository<TasksList> _repository;

        public UpdateTaskListHandler(IMediator mediatr, IRepository<TasksList> repository)
        {
            _mediatr = mediatr;
            _repository = repository;
        }

        public async Task<BaseResponseDto<UpdateTaskListResponse>> Handle(UpdateTaskListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await _repository.GetByGuidAsync(Guid.Parse(request.TaskListId));

                if (taskList is null)
                    return new BaseResponseDto<UpdateTaskListResponse>
                    {
                        Errors = new string[] { "Task list was not found" }
                    };

                taskList.Name = !string.IsNullOrEmpty(request.NewName) ? request.NewName : taskList.Name;
                taskList.Description = !string.IsNullOrEmpty(request.NewDescription) ? request.NewDescription : taskList.Description;

                await _repository.UpdateAsync(taskList);

                return new BaseResponseDto<UpdateTaskListResponse>
                {
                    Data = new UpdateTaskListResponse
                    {
                        IsUpdated = true,
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed update task list", ex);
            }
        }
    }
}
