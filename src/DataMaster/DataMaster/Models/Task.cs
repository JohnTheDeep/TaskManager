namespace DataMaster.Models;

public record Task : BaseEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public TaskStatus? Status { get; set; }
}
