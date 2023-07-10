using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Todo;
using Application.DTO.Todo.Enums;
using Application.IServices;
using AutoMapper;
using TodoV2.Domain.Entities;
using TodoV2.Domain.Enums;
using TodoV2.Infrastructure.IRepository;
using TodoV2.Utility.Errors;

namespace Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TodoDtoGet>> GetTodosAsync()
        {
            return _mapper.Map<List<TodoDtoGet>>(await _repository.GetTodosAsync());
        }

        public async Task<TodoDtoGet> GetTodoByGuidAsync(Guid todoGuid)
        {
            return _mapper.Map<TodoDtoGet>(await GetTodoEntity(todoGuid));
        }

        public async Task<TodoDtoGet> CreateTodoAsync(TodoDtoPost newTodo)
        {
            var todoEntity = _mapper.Map<Todo>(newTodo);
            todoEntity.Guid = Guid.NewGuid();
            todoEntity.CreatedAt = DateTime.Now;
            return _mapper.Map<TodoDtoGet>(await _repository.CreateTodoAsync(todoEntity));
        }

        public async Task<TodoDtoGet> UpdateTodoAsync(Guid todoGuid, TodoDtoPut newTodo)
        {
            var todoEntity = await GetTodoEntity(todoGuid, false);

            if (!string.IsNullOrEmpty(newTodo.Description))
            {
                todoEntity.Description = newTodo.Description;
            }

            if (!string.IsNullOrEmpty(newTodo.Title))
            {
                todoEntity.Title = newTodo.Title;
            }

            if (newTodo.Status != null)
            {
                todoEntity.Status = (Status)newTodo.Status;
            }

            return _mapper.Map<TodoDtoGet>(await _repository.UpdateTodoAsync(todoEntity));
        }

        public async Task<TodoDtoGet> UpdateTodoStatusAsync(Guid todoGuid, StatusDto statusDto)
        {
            var todoEntity = await GetTodoEntity(todoGuid, false);
            todoEntity.Status = (Status)statusDto;
            return _mapper.Map<TodoDtoGet>(await _repository.UpdateTodoAsync(todoEntity));
        }

        public async Task DeleteTodo(Guid todoGuid, bool hardDelete)
        {
            var todoEntity = await GetTodoEntity(todoGuid, false);
            if (hardDelete)
            {
                await _repository.DeleteTodo(todoEntity);
            }
            else
            {
                await _repository.SoftDeleteTodo(todoEntity);
            }
        }

        private async Task<Todo> GetTodoEntity(Guid todoGuid, bool asNoTracking = true)
        {
            var todoEntity = await _repository.GetTodoByGuidAsync(todoGuid, asNoTracking);
            if (todoEntity == default)
            {
                throw new NotFoundException("Todo não encontrado");
            }

            return todoEntity;
        }
    }
}