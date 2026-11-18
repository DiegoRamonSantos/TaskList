using System.ComponentModel.DataAnnotations;

namespace Tasklist.Models;

public class TaskItemInputModel
{
    [Display(Name = "Título")]
    [Required(ErrorMessage = "Informe um título para a tarefa.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 60 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Descrição")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string? Description { get; set; }

    [Display(Name = "Prioridade")]
    [Required(ErrorMessage = "Escolha uma prioridade.")]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Display(Name = "Prazo")]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }
}
