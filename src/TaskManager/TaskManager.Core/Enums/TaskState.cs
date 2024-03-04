using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Enums
{
    public enum TaskState
    {
        [Display(Name = "В ожидании")]
        AtPending = 0,

        [Display(Name = "Выполнена")]
        Completed = 1,

        [Display(Name = "В работе")]
        AtWork = 2
    }
}
