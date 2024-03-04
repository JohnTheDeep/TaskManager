using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public class UpdateTaskListRequest : IRequest<BaseResponseDto<UpdateTaskListResponse>>
{
    public string? NewName { get; set; }
    public string? NewDescription { get; set; }
    public string TaskListId { get; set; } = null!;
}
