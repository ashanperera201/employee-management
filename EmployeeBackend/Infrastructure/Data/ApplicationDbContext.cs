#region References
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
#endregion

#region Namespace
namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Employees> Employees { get; set; }
    }
}
#endregion