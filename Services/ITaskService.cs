using Tasklist.Models;

namespace Tasklist.Services;

public interface ITaskService
{
    Task<IReadOnlyList<TaskItem>> GetTasksAsync(TaskFilterDto filter, CancellationToken cancellationToken = default);

    Task<TaskStatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken = default);

    Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<TaskItem> CreateAsync(TaskItemInputModel input, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, TaskItemInputModel input, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> ToggleCompletionAsync(int id, CancellationToken cancellationToken = default);
}
