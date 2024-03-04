using DataMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace DataMaster.Db
{
    public class DataMasterDbContext : DbContext
    {
        public DataMasterDbContext()
        {
        }

        public DataMasterDbContext(DbContextOptions<DataMasterDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

    }
}
