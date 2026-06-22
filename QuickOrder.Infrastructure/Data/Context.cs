using Microsoft.EntityFrameworkCore;
using QuickOrder.Domain.Models;
namespace QuickOrder.Infrastructure.Data
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
    }
}
