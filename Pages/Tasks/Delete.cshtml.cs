using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasklist.Models;
using Tasklist.Services;

namespace Tasklist.Pages.Tasks;

public class DeleteModel(ITaskService taskService) : PageModel
{
    public TaskItem? TaskItem { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        TaskItem = await taskService.GetByIdAsync(id, cancellationToken);

        return TaskItem is null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        var deleted = await taskService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        TempData["Feedback"] = "Tarefa excluida com sucesso.";
        return RedirectToPage("/Index");
    }
}
