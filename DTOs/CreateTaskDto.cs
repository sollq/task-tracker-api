using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public required string Title { get; set; }

    [StringLength(500)] public string? Description { get; set; }

    [Required] public required DateTime StartDate { get; set; }

    [Required] public required DateTime EndDate { get; set; }

    public required string Username { get; set; }
}