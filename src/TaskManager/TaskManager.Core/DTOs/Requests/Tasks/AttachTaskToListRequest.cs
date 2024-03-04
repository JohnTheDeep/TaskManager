using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public class AttachTaskToListRequest : IRequest<BaseResponseDto<AttachTaskToListResponse>>
{
    /// <summary>
    /// ид списка задач
    /// </summary>
    public Guid TaskListId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
