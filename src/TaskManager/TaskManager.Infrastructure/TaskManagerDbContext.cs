using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
          : base(options)
        {
            Database.EnsureCreatedAsync().Wait();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Core.Models.Task> Tasks { get; set; }
        public DbSet<TasksList> TasksLists { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskStatusHistory> TaskHistory { get; set; }
    }
}
