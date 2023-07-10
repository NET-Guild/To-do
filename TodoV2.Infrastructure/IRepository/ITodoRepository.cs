using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoV2.Domain.Entities;
using TodoV2.Domain.Enums;

namespace TodoV2.Infrastructure.IRepository
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByGuidAsync(Guid todoGuid, bool asNoTracking = true);
        Task<Todo> CreateTodoAsync(Todo newTodo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task DeleteTodo(Todo todo);
        Task SoftDeleteTodo(Todo todo);
    }
}