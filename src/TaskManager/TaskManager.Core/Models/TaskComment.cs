namespace TaskManager.Core.Models;

public record TaskComment : BaseEntity
{
    public Task Task { get; set; }
    public string Comment { get; set; } = null!;
}
