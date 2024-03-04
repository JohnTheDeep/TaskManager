namespace TaskManager.Core.DTOs.Responses.Tasks;

public record GetTaskDetailsResponse : BaseResponse
{
    public TaskDetailsDTO Details { get; set; }
}
