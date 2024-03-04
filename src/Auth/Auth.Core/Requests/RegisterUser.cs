namespace Auth.Core.Requests;

public record RegisterUser
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
