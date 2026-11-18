using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasklist.Models;
using Tasklist.Services;

namespace Tasklist.Pages;

public class IndexModel(ITaskService taskService) : PageModel
{
    public TaskStatisticsDto Statistics { get; private set; } = new();

    public int CompletionPercentage { get; private set; }

    public int InProgress { get; private set; }

    public int DueToday { get; private set; }

    public int HighPriority { get; private set; }

    public IReadOnlyList<TaskItem> NextTasks { get; private set; } = [];

    public IReadOnlyList<TaskItem> UpcomingDeadlines { get; private set; } = [];

    public IReadOnlyList<DashboardActivity> RecentActivities { get; private set; } = [];

    public IReadOnlyList<DashboardBoardColumn> BoardColumns { get; private set; } = [];

    public IReadOnlyList<DashboardTaskGroup> TaskGroups { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Statistics = await taskService.GetStatisticsAsync(cancellationToken);
        var tasks = await taskService.GetTasksAsync(new TaskFilterDto(), cancellationToken);
        var today = DateTime.Today;
        var pendingTasks = tasks.Where(task => !task.IsCompleted).ToList();
        var activeTasks = pendingTasks
            .Where(task => IsHighPriority(task) || IsDueWithin(task, today, 7))
            .ToList();
        var todoTasks = pendingTasks.Except(activeTasks).ToList();
        var completedTasks = tasks.Where(task => task.IsCompleted).ToList();
        var orderedTasks = SortDashboardTasks(tasks);
        var orderedTodoTasks = SortDashboardTasks(todoTasks);
        var orderedActiveTasks = SortDashboardTasks(activeTasks);
        var orderedCompletedTasks = SortDashboardTasks(completedTasks);

        CompletionPercentage = Statistics.Total == 0
            ? 0
            : (int)Math.Round((double)Statistics.Completed / Statistics.Total * 100);
        InProgress = activeTasks.Count;
        DueToday = pendingTasks.Count(task => task.DueDate?.Date == today);
        HighPriority = pendingTasks.Count(IsHighPriority);

        NextTasks = pendingTasks
            .OrderBy(task => task.DueDate ?? DateTime.MaxValue)
            .ThenByDescending(task => task.Priority)
            .ThenByDescending(task => task.CreatedAt)
            .Take(4)
            .ToList();

        UpcomingDeadlines = pendingTasks
            .Where(task => task.DueDate.HasValue)
            .OrderBy(task => task.DueDate)
            .ThenByDescending(task => task.Priority)
            .ToList();

        RecentActivities = tasks
            .OrderByDescending(task => task.CreatedAt)
            .Select(task => new DashboardActivity(
                task.IsCompleted ? "Tarefa concluida" : "Tarefa criada",
                task.Title,
                task.CreatedAt,
                task.IsCompleted ? "success" : GetActivityTone(task)))
            .ToList();

        TaskGroups =
        [
            new DashboardTaskGroup("all", "Todas", orderedTasks),
            new DashboardTaskGroup("todo", "A fazer", orderedTodoTasks),
            new DashboardTaskGroup("doing", "Em andamento", orderedActiveTasks),
            new DashboardTaskGroup("done", "Concluídas", orderedCompletedTasks)
        ];

        BoardColumns =
        [
            new DashboardBoardColumn("A fazer", "todo", orderedTodoTasks, orderedTodoTasks.Count),
            new DashboardBoardColumn("Em andamento", "doing", orderedActiveTasks, orderedActiveTasks.Count),
            new DashboardBoardColumn("Concluído", "done", orderedCompletedTasks, orderedCompletedTasks.Count)
        ];
    }

    public string GetDueLabel(DateTime? dueDate)
    {
        if (!dueDate.HasValue)
        {
            return "Sem prazo";
        }

        var days = (dueDate.Value.Date - DateTime.Today).Days;

        return days switch
        {
            < 0 => "Atrasada",
            0 => "Hoje",
            1 => "Amanhã",
            <= 7 => $"Em {days} dias",
            _ => dueDate.Value.ToString("dd/MM/yyyy")
        };
    }

    public string FormatRelativeTime(DateTime dateTime)
    {
        var localDateTime = dateTime.Kind == DateTimeKind.Utc ? dateTime.ToLocalTime() : dateTime;
        var elapsed = DateTime.Now - localDateTime;

        if (elapsed.TotalMinutes < 1)
        {
            return "Agora";
        }

        if (elapsed.TotalHours < 1)
        {
            var minutes = Math.Max(1, (int)Math.Floor(elapsed.TotalMinutes));
            return $"Ha {minutes} min";
        }

        if (elapsed.TotalDays < 1)
        {
            var hours = Math.Max(1, (int)Math.Floor(elapsed.TotalHours));
            return $"Ha {hours} h";
        }

        if (elapsed.TotalDays < 30)
        {
            var days = Math.Max(1, (int)Math.Floor(elapsed.TotalDays));
            return $"Ha {days} dias";
        }

        return localDateTime.ToString("dd/MM/yyyy");
    }

    private static bool IsDueWithin(TaskItem task, DateTime today, int days)
    {
        if (!task.DueDate.HasValue)
        {
            return false;
        }

        var dueInDays = (task.DueDate.Value.Date - today).Days;
        return dueInDays >= 0 && dueInDays <= days;
    }

    private static bool IsHighPriority(TaskItem task)
    {
        return task.Priority is TaskPriority.High or TaskPriority.Urgent;
    }

    private static string GetActivityTone(TaskItem task)
    {
        return task.Priority switch
        {
            TaskPriority.High or TaskPriority.Urgent => "danger",
            TaskPriority.Medium => "warning",
            _ => "info"
        };
    }

    private static IReadOnlyList<TaskItem> SortDashboardTasks(IEnumerable<TaskItem> tasks)
    {
        return tasks
            .OrderBy(task => task.IsCompleted)
            .ThenBy(task => task.DueDate ?? DateTime.MaxValue)
            .ThenByDescending(task => task.Priority)
            .ThenByDescending(task => task.CreatedAt)
            .ToList();
    }
}

public sealed record DashboardActivity(string Action, string TaskTitle, DateTime OccurredAt, string Tone);

public sealed record DashboardBoardColumn(string Title, string Tone, IReadOnlyList<TaskItem> Tasks, int Count);

public sealed record DashboardTaskGroup(string Key, string Title, IReadOnlyList<TaskItem> Tasks);
