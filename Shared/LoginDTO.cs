using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record LoginDTO
    {
        [EmailAddress]
        public string Email { get; init; }
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        public string Password { get; init; }
    }
}
