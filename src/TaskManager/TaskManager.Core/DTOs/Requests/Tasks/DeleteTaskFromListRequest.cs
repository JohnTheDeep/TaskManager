using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public record DeleteTaskFromListRequest : IRequest<BaseResponseDto<DeleteTaskFromListResponse>>
{
    public string TaskId { get; set; } = null!;
}
