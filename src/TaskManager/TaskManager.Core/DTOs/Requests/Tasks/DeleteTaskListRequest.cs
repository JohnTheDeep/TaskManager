using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public record DeleteTaskListRequest : IRequest<BaseResponseDto<DeleteTaskListResponse>>
{
    public string TaskListId { get; set; } = null!;
}
