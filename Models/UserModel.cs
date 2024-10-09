using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5)]
    public required string Username { get; set; }

    [Required]
    [StringLength(5000, MinimumLength = 8)]
    public required string Password { get; set; }

    public ICollection<TaskModel>? Tasks { get; set; }
}