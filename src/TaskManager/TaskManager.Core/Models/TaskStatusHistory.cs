﻿using TaskManager.Core.Enums;

namespace TaskManager.Core.Models
{
    public record TaskStatusHistory : BaseEntity
    {
        public TaskState Status { get; set; }
        public TaskState PreviousStatus { get; set; }
        public Task Task { get; set; } = null!;
    }
}
