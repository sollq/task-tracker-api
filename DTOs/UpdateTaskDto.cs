using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.DTOs;

public class UpdateTaskDto
{
    [StringLength(5000)] public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    [StringLength(100, MinimumLength = 5)] public required string Title { get; set; }
}