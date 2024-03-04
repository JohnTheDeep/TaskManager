namespace Auth.Core.Models;

public record BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime ModifiedAt { get; set; }
}
