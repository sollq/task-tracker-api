using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.DTOs;

public class LoginDto
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Username { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 8)]
    public required string Password { get; set; }
}