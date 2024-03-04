namespace TaskManager.Core.DTOs.Responses.Tasks;

public record UpdateTaskListResponse : BaseResponse
{
    public bool IsUpdated { get; set; }
}
