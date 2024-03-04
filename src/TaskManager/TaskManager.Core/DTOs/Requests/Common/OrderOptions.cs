using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs.Requests.Common;

public record OrderOptions
{
    /// <summary>
    /// Наименование поля для сортировки
    /// </summary>
    public string? OrderPropertyName { get; set; }

    /// <summary>
    /// Тип, возрастание/убывание
    /// </summary>
    public OrderBy? OrderBy { get; set; }
}
