using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasklist.Models;
using Tasklist.Services;

namespace Tasklist.Pages.Tasks;

public class EditModel(ITaskService taskService) : PageModel
{
    [BindProperty]
    public TaskItemInputModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var task = await taskService.GetByIdAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        Input = new TaskItemInputModel
        {
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            DueDate = task.DueDate
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updated = await taskService.UpdateAsync(id, Input, cancellationToken);

        if (!updated)
        {
            return NotFound();
        }

        TempData["Feedback"] = "Tarefa atualizada com sucesso.";
        return RedirectToPage("/Index");
    }
}
