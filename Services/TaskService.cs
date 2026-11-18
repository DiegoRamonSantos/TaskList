using Microsoft.EntityFrameworkCore;
using Tasklist.Data;
using Tasklist.Models;

namespace Tasklist.Services;

public class TaskService(ApplicationDbContext context) : ITaskService
{
    public async Task<IReadOnlyList<TaskItem>> GetTasksAsync(TaskFilterDto filter, CancellationToken cancellationToken = default)
    {
        var query = context.TaskItems.AsNoTracking();
        query = ApplyFilters(query, filter);
        query = ApplySorting(query, filter.Sort);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TaskStatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var total = await context.TaskItems.CountAsync(cancellationToken);
        var completed = await context.TaskItems.CountAsync(task => task.IsCompleted, cancellationToken);

        return new TaskStatisticsDto
        {
            Total = total,
            Completed = completed,
            Pending = total - completed
        };
    }

    public Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.TaskItems.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id, cancellationToken);
    }

    public async Task<TaskItem> CreateAsync(TaskItemInputModel input, CancellationToken cancellationToken = default)
    {
        var task = new TaskItem
        {
            Title = NormalizeRequired(input.Title),
            Description = NormalizeOptional(input.Description),
            Priority = input.Priority,
            DueDate = NormalizeDate(input.DueDate),
            CreatedAt = DateTime.UtcNow
        };

        context.TaskItems.Add(task);
        await context.SaveChangesAsync(cancellationToken);

        return task;
    }

    public async Task<bool> UpdateAsync(int id, TaskItemInputModel input, CancellationToken cancellationToken = default)
    {
        var task = await context.TaskItems.FindAsync(new object[] { id }, cancellationToken);

        if (task is null)
        {
            return false;
        }

        task.Title = NormalizeRequired(input.Title);
        task.Description = NormalizeOptional(input.Description);
        task.Priority = input.Priority;
        task.DueDate = NormalizeDate(input.DueDate);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var task = await context.TaskItems.FindAsync(new object[] { id }, cancellationToken);

        if (task is null)
        {
            return false;
        }

        context.TaskItems.Remove(task);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ToggleCompletionAsync(int id, CancellationToken cancellationToken = default)
    {
        var task = await context.TaskItems.FindAsync(new object[] { id }, cancellationToken);

        if (task is null)
        {
            return false;
        }

        task.IsCompleted = !task.IsCompleted;
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static IQueryable<TaskItem> ApplyFilters(IQueryable<TaskItem> query, TaskFilterDto filter)
    {
        var search = NormalizeOptional(filter.Search);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = $"%{search}%";
            query = query.Where(task =>
                EF.Functions.Like(task.Title, term) ||
                (task.Description != null && EF.Functions.Like(task.Description, term)));
        }

        query = filter.Status switch
        {
            TaskStatusFilter.Completed => query.Where(task => task.IsCompleted),
            TaskStatusFilter.Pending => query.Where(task => !task.IsCompleted),
            _ => query
        };

        if (filter.Priority.HasValue)
        {
            query = query.Where(task => task.Priority == filter.Priority.Value);
        }

        if (filter.DueFrom.HasValue)
        {
            var from = filter.DueFrom.Value.Date;
            query = query.Where(task => task.DueDate >= from);
        }

        if (filter.DueTo.HasValue)
        {
            var to = filter.DueTo.Value.Date.AddDays(1);
            query = query.Where(task => task.DueDate < to);
        }

        return query;
    }

    private static IQueryable<TaskItem> ApplySorting(IQueryable<TaskItem> query, TaskSortOption sort)
    {
        return sort switch
        {
            TaskSortOption.CreatedAtAsc => query.OrderBy(task => task.CreatedAt),
            TaskSortOption.DueDateAsc => query.OrderBy(task => task.DueDate == null).ThenBy(task => task.DueDate),
            TaskSortOption.DueDateDesc => query.OrderBy(task => task.DueDate == null).ThenByDescending(task => task.DueDate),
            TaskSortOption.PriorityAsc => query.OrderBy(task => task.Priority).ThenByDescending(task => task.CreatedAt),
            TaskSortOption.PriorityDesc => query.OrderByDescending(task => task.Priority).ThenByDescending(task => task.CreatedAt),
            _ => query.OrderByDescending(task => task.CreatedAt)
        };
    }

    private static string NormalizeRequired(string value)
    {
        return value.Trim();
    }

    private static string? NormalizeOptional(string? value)
    {
        var normalized = value?.Trim();
        return string.IsNullOrWhiteSpace(normalized) ? null : normalized;
    }

    private static DateTime? NormalizeDate(DateTime? value)
    {
        return value?.Date;
    }
}