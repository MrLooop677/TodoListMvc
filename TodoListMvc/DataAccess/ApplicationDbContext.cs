using Microsoft.EntityFrameworkCore;
using TodoListMvc.Models;

namespace TodoListMvc.DataAccess
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItemes { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PC-46;Initial Catalog=TodoApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }

    }


}
