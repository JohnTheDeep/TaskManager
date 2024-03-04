namespace TaskManager.Core.DTOs.Responses.Tasks;

public record AttachTaskToListResponse : BaseResponse
{
    public Guid id { get; set; }
}
