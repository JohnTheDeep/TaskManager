using System.Text.Json.Serialization;

namespace DataMaster.Models;

public record User : BaseEntity
{
    /// <summary>
    /// Имя
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Почта
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    /// Хэш пароля
    /// </summary>
    [JsonIgnore]
    public string? PasswordHash { get; set; }
}
