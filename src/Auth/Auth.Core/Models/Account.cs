namespace Auth.Core.Models;

public record Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}
