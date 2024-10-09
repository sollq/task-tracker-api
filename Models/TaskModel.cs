using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.Models;

public class TaskModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Title { get; set; }

    [StringLength(5000, MinimumLength = 0)]
    public string? Description { get; set; }

    [Required] public required DateTime StartDate { get; set; }

    [Required] public required DateTime EndDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }

    [Required] public required bool IsCompleted { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public required string Username { get; set; }
}