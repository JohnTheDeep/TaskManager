namespace TaskManager.Core.DTOs.Requests.Common;

public record SearchOptions
{
    /// <summary>
    /// Пагинация
    /// </summary>
    public int? From { get; set; }

    /// <summary>
    /// Пагинация
    /// </summary>
    public int? Size { get; set; } = 20;

    /// <summary>
    /// Опции сортировки (множество)
    /// </summary>
    public List<OrderOptions> OrderOptions { get; set; } = new();
}
