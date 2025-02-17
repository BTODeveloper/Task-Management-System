using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs
{
    public record LoginDTO
    {
        [Required]
        [MinLength(3)]
        public string Username { get; init; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }

    public record RegisterDTO
    {
        [Required]
        [MinLength(3)]
        public string Username { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }
}