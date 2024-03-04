using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, BaseResponseDto<UpdateTaskResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Models.Task> _repository;
        private readonly IRepository<TaskStatusHistory> _historyRepository;

        public UpdateTaskHandler(IMediator mediator, IRepository<Models.Task> repository, IRepository<TaskStatusHistory> historyRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _historyRepository = historyRepository;
        }

        public async Task<BaseResponseDto<UpdateTaskResponse>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _repository.GetByGuidAsync(Guid.Parse(request.TaskId));

                TaskState prevState = task.State;

                if (task is null)
                    return new BaseResponseDto<UpdateTaskResponse>
                    {
                        Errors = new string[] { "Task was not fonud" }
                    };

                task.Name = !string.IsNullOrEmpty(request.NewName) ? request.NewName : task.Name;
                task.Description = !string.IsNullOrEmpty(request.NewDescription) ? request.NewDescription : task.Description;
                task.State = request.NewState.HasValue ? request.NewState.Value : task.State;

                await _repository.UpdateAsync(task);

                var historyAction = new TaskStatusHistory
                {
                    PreviousStatus = prevState,
                    Status = task.State,
                    Task = task,
                    CreatedAt = DateTime.UtcNow
                };

                await _historyRepository.CreateAsync(historyAction);


                return new BaseResponseDto<UpdateTaskResponse>
                {
                    Data = new UpdateTaskResponse
                    {
                        IsUpdated = true,
                        StatusCode = 200,
                        Task = task
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Task", ex);
            }
        }
    }
}
