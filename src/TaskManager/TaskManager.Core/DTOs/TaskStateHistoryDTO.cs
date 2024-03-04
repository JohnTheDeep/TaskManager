using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs;

public record TaskStateHistoryDTO
{
    public Guid id { get; set; }
    public TaskState State { get; set; }
    public TaskState PrevState { get; set; }
    public DateTime CreatedDate { get; set; }
}
