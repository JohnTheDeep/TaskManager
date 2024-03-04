namespace TaskManager.Core.DTOs.Responses.Tasks;

public record UpdateTaskResponse : BaseResponse
{
    public bool IsUpdated { get; set; }
    public Models.Task Task { get; set; }
}
