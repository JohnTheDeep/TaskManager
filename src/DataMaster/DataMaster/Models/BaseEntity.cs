namespace DataMaster.Models;

public abstract record BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Дата редактирования записи
    /// </summary>
    public DateTime ModifiedDate { get; set; }
}
