using TaskManager.Core.Models;

namespace TaskManager.Core.DTOs.Responses.Tasks;

public record GetTasksListResponse : BaseResponse
{
    public List<TasksList> MyTasks { get; set; } = new();
}
