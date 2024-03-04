using Auth.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Storage
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
    }
}
