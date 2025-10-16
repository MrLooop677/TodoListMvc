using Microsoft.EntityFrameworkCore;
using TodoListMvc.Models;

namespace TodoListMvc.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItemes { get; set; }
    }
}
