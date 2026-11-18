using System.ComponentModel.DataAnnotations;

namespace Tasklist.Models;

public enum TaskPriority
{
    [Display(Name = "Baixa")]
    Low = 1,

    [Display(Name = "Media")]
    Medium = 2,

    [Display(Name = "Alta")]
    High = 3,

    [Display(Name = "Urgente")]
    Urgent = 4
}
