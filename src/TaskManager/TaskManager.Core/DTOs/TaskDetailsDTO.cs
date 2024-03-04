namespace TaskManager.Core.DTOs;

public record TaskDetailsDTO
{
    public Models.Task Task { get; set; }
    public List<TaskCommentDto> Comments { get; set; }
    public List<TaskStateHistoryDTO> StateHistory { get; set; }
}
