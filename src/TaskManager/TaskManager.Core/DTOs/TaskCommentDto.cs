namespace TaskManager.Core.DTOs;

public record TaskCommentDto
{
    public Guid id { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}
