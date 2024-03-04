namespace TaskManager.Core.DTOs.Responses;

public class BaseResponseDto<TData>
{
    public string[] Errors { get; set; } = Array.Empty<string>();
    public TData? Data { get; set; }
    public bool HasErrors => Errors.Any();
}
