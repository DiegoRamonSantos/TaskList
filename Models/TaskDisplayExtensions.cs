namespace Tasklist.Models;

public static class TaskDisplayExtensions
{
    public static string ToDisplayName(this TaskPriority priority)
    {
        return priority switch
        {
            TaskPriority.Low => "Baixa",
            TaskPriority.Medium => "Media",
            TaskPriority.High => "Alta",
            TaskPriority.Urgent => "Urgente",
            _ => priority.ToString()
        };
    }

    public static string ToCssClass(this TaskPriority priority)
    {
        return priority switch
        {
            TaskPriority.Low => "priority-low",
            TaskPriority.Medium => "priority-medium",
            TaskPriority.High => "priority-high",
            TaskPriority.Urgent => "priority-urgent",
            _ => "priority-medium"
        };
    }

    public static string ToDisplayName(this TaskStatusFilter status)
    {
        return status switch
        {
            TaskStatusFilter.Pending => "Pendentes",
            TaskStatusFilter.Completed => "Concluídas",
            _ => "Todas"
        };
    }

    public static string ToDisplayName(this TaskSortOption sort)
    {
        return sort switch
        {
            TaskSortOption.CreatedAtAsc => "Criação mais antiga",
            TaskSortOption.DueDateAsc => "Prazo mais próximo",
            TaskSortOption.DueDateDesc => "Prazo mais distante",
            TaskSortOption.PriorityDesc => "Maior prioridade",
            TaskSortOption.PriorityAsc => "Menor prioridade",
            _ => "Criação recente"
        };
    }
}
