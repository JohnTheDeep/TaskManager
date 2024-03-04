namespace TaskManager.Core.DTOs.Responses.Tasks;

public record CreateTasksListResponse : BaseResponse
{
    public Guid Guid { get; set; }
}
