namespace Auth.Core.Models;

public record TasksList : BaseEntity
{
    public User User { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<Task> AttachedTask { get; set; } = new();
}
