using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class DeleteTaskListHandler : IRequestHandler<DeleteTaskListRequest, BaseResponseDto<DeleteTaskListResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<TasksList> _repository;

        public DeleteTaskListHandler(IMediator mediator, IRepository<TasksList> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<BaseResponseDto<DeleteTaskListResponse>> Handle(DeleteTaskListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new BaseResponseDto<DeleteTaskListResponse>();

                var taskList = await _repository.GetByGuidAsync(Guid.Parse(request.TaskListId));

                if (taskList is null)
                    return new BaseResponseDto<DeleteTaskListResponse>
                    {
                        Errors = new string[] { "TaskList was not found" }
                    };

                if (taskList.AttachedTask?.Any() is true)
                    return new BaseResponseDto<DeleteTaskListResponse>
                    {
                        Errors = new string[] { "Cant delete unempty task list" }
                    };

                await _repository.DeleteAsync(taskList);

                return new BaseResponseDto<DeleteTaskListResponse>
                {
                    Data = new DeleteTaskListResponse
                    {
                        IsDeleted = true,
                        Response = "Task list was deleted",
                        StatusCode = 200
                    }
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Failed delete Task List", ex);
            }
        }
    }
}
