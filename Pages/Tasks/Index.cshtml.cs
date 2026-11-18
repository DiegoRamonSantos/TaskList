using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Tasklist.Models;
using Tasklist.Services;

namespace Tasklist.Pages.Tasks;

public class IndexModel(ITaskService taskService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public TaskFilterDto Filters { get; set; } = new();

    public IReadOnlyList<TaskItem> Tasks { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Tasks = await taskService.GetTasksAsync(Filters, cancellationToken);
    }

    public async Task<IActionResult> OnPostToggleAsync(int id, CancellationToken cancellationToken)
    {
        var updated = await taskService.ToggleCompletionAsync(id, cancellationToken);
        TempData["Feedback"] = updated ? "Status da tarefa atualizado." : "Tarefa não encontrada.";

        return RedirectToPage("/Tasks/Index", BuildFilterRouteValues());
    }

    private RouteValueDictionary BuildFilterRouteValues()
    {
        var values = new RouteValueDictionary
        {
            ["Filters.Status"] = Filters.Status,
            ["Filters.Sort"] = Filters.Sort
        };

        if (!string.IsNullOrWhiteSpace(Filters.Search))
        {
            values["Filters.Search"] = Filters.Search;
        }

        if (Filters.Priority.HasValue)
        {
            values["Filters.Priority"] = Filters.Priority.Value;
        }

        if (Filters.DueFrom.HasValue)
        {
            values["Filters.DueFrom"] = Filters.DueFrom.Value.ToString("yyyy-MM-dd");
        }

        if (Filters.DueTo.HasValue)
        {
            values["Filters.DueTo"] = Filters.DueTo.Value.ToString("yyyy-MM-dd");
        }

        return values;
    }
}
