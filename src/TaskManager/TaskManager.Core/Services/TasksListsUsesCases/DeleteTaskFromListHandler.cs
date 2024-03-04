using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class DeleteTaskFromListHandler : IRequestHandler<DeleteTaskFromListRequest, BaseResponseDto<DeleteTaskFromListResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Models.Task> _repository;

        public DeleteTaskFromListHandler(IMediator mediator, IRepository<Models.Task> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<BaseResponseDto<DeleteTaskFromListResponse>> Handle(DeleteTaskFromListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _repository.GetSingleBy(_ => _.id == Guid.Parse(request.TaskId));

                if (task is null)
                    return new BaseResponseDto<DeleteTaskFromListResponse>
                    {
                        Data = new DeleteTaskFromListResponse
                        {
                            IsDeleted = false,
                            StatusCode = 500
                        },
                        Errors = new string[] { "Task was not found" }
                    };

                await _repository.DeleteAsync(task);

                return new BaseResponseDto<DeleteTaskFromListResponse>
                {
                    Data = new DeleteTaskFromListResponse
                    {
                        IsDeleted = true,
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed delete task from list", ex);
            }
        }
    }
}
