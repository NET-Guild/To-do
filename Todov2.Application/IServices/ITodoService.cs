using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Todo;
using Application.DTO.Todo.Enums;

namespace Application.IServices
{
    public interface ITodoService
    {
        Task<List<TodoDtoGet>> GetTodosAsync();
        Task<TodoDtoGet> GetTodoByGuidAsync(Guid todoGuid);
        Task<TodoDtoGet> CreateTodoAsync(TodoDtoPost newTodo);
        Task<TodoDtoGet> UpdateTodoAsync(Guid todoGuid, TodoDtoPut newTodo);
        Task<TodoDtoGet> UpdateTodoStatusAsync(Guid todoGuid, StatusDto statusDto);
        Task DeleteTodo(Guid todoGuid, bool hardDelete);
    }
}