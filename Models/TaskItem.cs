using System.ComponentModel.DataAnnotations;

namespace Tasklist.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? DueDate { get; set; }
}
