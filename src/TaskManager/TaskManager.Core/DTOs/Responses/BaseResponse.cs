namespace TaskManager.Core.DTOs.Responses;

public abstract record BaseResponse
{
    public int StatusCode { get; set; }
}
