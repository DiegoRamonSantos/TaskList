using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasklist.Models;
using Tasklist.Services;

namespace Tasklist.Pages.Tasks;

public class CreateModel(ITaskService taskService) : PageModel
{
    [BindProperty]
    public TaskItemInputModel Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await taskService.CreateAsync(Input, cancellationToken);
        TempData["Feedback"] = "Tarefa criada com sucesso.";

        return RedirectToPage("/Index");
    }
}
