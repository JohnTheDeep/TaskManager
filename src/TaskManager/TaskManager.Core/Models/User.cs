namespace TaskManager.Core.Models
{
    public record User : BaseEntity
    {
        /// <summary>
        /// Фио
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = null!;
    }
}
