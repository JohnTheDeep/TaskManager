﻿using TaskManager.Core.Enums;

namespace TaskManager.Core.Models;

public record Task : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public TaskState State { get; set; }
    public TasksList? List { get; set; }
}
