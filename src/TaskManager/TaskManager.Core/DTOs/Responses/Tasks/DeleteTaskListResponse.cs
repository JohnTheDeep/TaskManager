namespace TaskManager.Core.DTOs.Responses.Tasks;

public record DeleteTaskListResponse : BaseResponse
{
    public bool IsDeleted { get; set; }
    public string? Response { get; set; }
}
