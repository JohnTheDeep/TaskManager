using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public record MergeTaskListRequest : IRequest<BaseResponseDto<MergeTaskListResponse>>
{
    /// <summary>
    /// Список куда мержить
    /// </summary>
    public string? ListDestinationId { get; set; }

    /// <summary>
    /// Список откуда мержить
    /// </summary>
    public string? ListSourceId { get; set; }
}
