using Auth.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Storage
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
          : base(options)
        {
            Database.EnsureCreatedAsync().Wait();
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
