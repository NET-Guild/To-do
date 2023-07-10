using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoV2.Domain.Entities;
using TodoV2.Domain.Enums;
using TodoV2.Infrastructure.DataBase;
using TodoV2.Infrastructure.IRepository;

namespace TodoV2.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        public async Task<List<Todo>> GetTodosAsync()
        {
            await using var context = new Context();
            return await context.Todos.Where(t => t.Status != Status.Deleted).AsNoTracking().ToListAsync();
        }

        public async Task<Todo> GetTodoByGuidAsync(Guid todoGuid, bool asNoTracking = true)
        {
            await using var context = new Context();
            var todoQueryable = context.Todos.AsQueryable();

            if (asNoTracking)
            {
                todoQueryable.AsNoTracking();
            }

            return await todoQueryable.FirstOrDefaultAsync(t => t.Guid == todoGuid);
        }

        public async Task<Todo> CreateTodoAsync(Todo newTodo)
        {
            await using var context = new Context();
            await context.AddAsync(newTodo);
            await context.SaveChangesAsync();
            return newTodo;
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            await using var context = new Context();
            context.Update(todo);
            await context.SaveChangesAsync();
            return todo;
        }

        public async Task DeleteTodo(Todo todo)
        {
            await using var context = new Context();
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
        }

        public async Task SoftDeleteTodo(Todo todo)
        {
            await using var context = new Context();
            todo.Status = Status.Deleted;
            context.Update(todo);
            await context.SaveChangesAsync();
        }
    }
}