using System.ComponentModel.DataAnnotations;

namespace DataMaster.Enums;

public enum TaskStatus
{
    [Display(Name = "В ожидании")]
    Pending = 1,

    [Display(Name = "В работе")]
    AtWork = 2,

    [Display(Name = "Завершена")]
    Done = 3
}
