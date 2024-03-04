using MediatR;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public class UpdateTaskRequest : IRequest<BaseResponseDto<UpdateTaskResponse>>
{
    public TaskState? NewState { get; set; }
    public string? NewName { get; set; }
    public string? NewDescription { get; set; }
    public string TaskId { get; set; } = null!;
}
