namespace TaskManager.Core.DTOs.Responses.Tasks;

public record DeleteTaskFromListResponse : BaseResponse
{
    public bool IsDeleted { get; set; }
}
