namespace Tasklist.Models;

public class TaskFilterDto
{
    public string? Search { get; set; }

    public TaskStatusFilter Status { get; set; } = TaskStatusFilter.All;

    public TaskPriority? Priority { get; set; }

    public DateTime? DueFrom { get; set; }

    public DateTime? DueTo { get; set; }

    public TaskSortOption Sort { get; set; } = TaskSortOption.CreatedAtDesc;
}
