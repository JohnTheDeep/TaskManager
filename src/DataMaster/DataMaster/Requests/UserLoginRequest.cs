using System.ComponentModel.DataAnnotations;

namespace DataMaster.Requests;

public class UserLoginRequest
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
