namespace Tasklist.Models;

public class TaskStatisticsDto
{
    public int Total { get; init; }

    public int Completed { get; init; }

    public int Pending { get; init; }
}
