using System.ComponentModel.DataAnnotations;

namespace DataMaster.Requests
{
    public class UserCreateRequest
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        [StringLength(100, MinimumLength = 4)]
        public string Login { get; set; } = null!;

        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}
