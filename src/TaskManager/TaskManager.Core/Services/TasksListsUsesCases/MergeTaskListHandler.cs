using MediatR;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class MergeTaskListHandler : IRequestHandler<MergeTaskListRequest, BaseResponseDto<MergeTaskListResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Models.Task> _repository;
        private readonly IRepository<TasksList> _tasksRepository;

        public MergeTaskListHandler(
            IMediator mediator,
            IRepository<Models.Task> repository,
            IRepository<TasksList> tasksRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _tasksRepository = tasksRepository;
        }

        public async Task<BaseResponseDto<MergeTaskListResponse>> Handle(MergeTaskListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var taskListSource = await _tasksRepository.GetByGuidAsync(Guid.Parse(request.ListSourceId));
                var taskListDestination = await _tasksRepository.GetByGuidAsync(Guid.Parse(request.ListDestinationId));

                var sourceTasks = await _repository.GetWhereAsync(_ => _.List == taskListSource);

                sourceTasks.ForEach(_ => _.List = taskListDestination);

                await _repository.UpdateRangeAsync(sourceTasks);

                return new BaseResponseDto<MergeTaskListResponse>
                {
                    Data = new MergeTaskListResponse
                    {
                        List = taskListDestination,
                        StatusCode = 200
                    }
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Failed merge lists", ex);
            }
        }
    }
}
