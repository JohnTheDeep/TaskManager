using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Core.DTOs;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class GetTaskDetailsHandler : IRequestHandler<GetTaskDetailsRequest, BaseResponseDto<GetTaskDetailsResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Models.Task> _repository;
        private readonly IRepository<TaskComment> _commentsRepository;
        private readonly IRepository<TaskStatusHistory> _statusHistoryRepository;

        public GetTaskDetailsHandler(
            IMediator mediator,
            IRepository<Models.Task> repository,
            IRepository<TaskComment> commentsRepository,
            IRepository<TaskStatusHistory> statusHistoryRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _commentsRepository = commentsRepository;
            _statusHistoryRepository = statusHistoryRepository;
        }

        public async Task<BaseResponseDto<GetTaskDetailsResponse>> Handle(GetTaskDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _repository.GetByGuidAsync(Guid.Parse(request.TaskId));

                if (task is null)
                    return new BaseResponseDto<GetTaskDetailsResponse>
                    {
                        Errors = new[] { "Task was not founded" }
                    };

                var taskHistory = await _statusHistoryRepository.GetWhereAsync(_ => _.Task == task);

                var comments = await _commentsRepository.GetWhereAsync(_ => _.Task == task);

                var details = new TaskDetailsDTO
                {
                    Task = task,
                    Comments = comments.Select(_ => new TaskCommentDto
                    {
                        Comment = _.Comment,
                        CreatedDate = _.CreatedAt,
                        id = _.id
                    }).ToList(),
                    StateHistory = taskHistory.Select(_ => new TaskStateHistoryDTO
                    {
                        CreatedDate = _.CreatedAt,
                        id = _.id,
                        PrevState = _.PreviousStatus,
                        State = _.Status
                    }).ToList()
                };

                return new BaseResponseDto<GetTaskDetailsResponse>
                {
                    Data = new GetTaskDetailsResponse
                    {
                        Details = details,
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed Get Task Details", ex);
            }
        }
    }
}
