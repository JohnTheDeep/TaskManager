using MediatR;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Requests.Tasks;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using System.ComponentModel;

namespace TaskManager.Core.Services.TasksListsUsesCases
{
    public class GetMyTasksListHandler : IRequestHandler<GetTasksListRequest, BaseResponseDto<GetTasksListResponse>>
    {
        private readonly IRepository<TasksList> _repository;

        public GetMyTasksListHandler(IRepository<TasksList> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponseDto<GetTasksListResponse>> Handle(GetTasksListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId)) new BaseResponseDto<GetTasksListResponse> { Errors = new string[] { "Invalid UserId" } };
                var response = new GetTasksListResponse();

                if (!string.IsNullOrEmpty(request.PropertyFilter) && !string.IsNullOrEmpty(request.PropertyValue))
                {
                    var prop = TypeDescriptor.GetProperties(typeof(TasksList)).Find(request.PropertyFilter, true);

                    if (prop is null)
                        return new BaseResponseDto<GetTasksListResponse> { Errors = new string[] { "Invalid property name for filter" } };

                    response.MyTasks = (await _repository.GetWhereAsync(
                        predicate: x =>
                            prop.Name == "Name" ? x.Name == request.PropertyValue : false ||
                            prop.Name == "Description" ? x.Description == request.PropertyValue : false &&
                            x.User.id == Guid.Parse(request.UserId),
                        searchOptions: request.SearchOption)).ToList();
                }
                else
                {
                    response.MyTasks = (await _repository.GetListAsync(
                        searchOptions: request.SearchOption)).ToList();
                }

                return new BaseResponseDto<GetTasksListResponse> { Data = response };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed get tasks list", ex);
            }
        }
    }
}
