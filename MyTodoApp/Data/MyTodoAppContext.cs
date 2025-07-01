using Microsoft.EntityFrameworkCore;
using MyTodoApp.Models;

namespace MyTodoApp.Data
{
    public class MyTodoAppContext : DbContext
    {
        public MyTodoAppContext(DbContextOptions<MyTodoAppContext> options) : base(options) { }

        public DbSet<Todo> todos { get; set; }
        public DbSet<User> users { get; set; }
    }
}
