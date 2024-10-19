using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record RegisterDTO
    {
        [EmailAddress]
        public string Email { get; init; }
        public string DisplayName { get; init; }
        public string UserName { get; init; }
        [Phone]
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
    }

}
