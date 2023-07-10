using TodoV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoV2.Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Todos.db");
        }
    }
}