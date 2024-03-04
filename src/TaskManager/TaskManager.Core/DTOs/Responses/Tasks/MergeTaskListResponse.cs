using TaskManager.Core.Models;

namespace TaskManager.Core.DTOs.Responses.Tasks;

public record MergeTaskListResponse : BaseResponse
{
    public TasksList List { get; set; }
}
