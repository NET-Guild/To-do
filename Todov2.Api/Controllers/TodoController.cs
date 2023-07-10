using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Todo;
using Application.DTO.Todo.Enums;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Todov2.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all Todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TodoDtoGet>>> GetTodos()
        {
            var result = await _service.GetTodosAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return todos with the same GUID
        /// </summary>
        /// <param name="todoGuid">Todo Guid for comparision</param>
        /// <returns></returns>
        [HttpGet("{todoGuid:Guid}")]
        public async Task<ActionResult<TodoDtoGet>> GetTodosById([FromRoute] Guid todoGuid)
        {
            return Ok(await _service.GetTodoByGuidAsync(todoGuid));
        }

        /// <summary>
        /// Create a new todo and returns
        /// </summary>
        /// <param name="todoDto">Todo DTO to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoDtoGet>> CreateTodo([FromBody] TodoDtoPost todoDto)
        {
            var todo = await _service.CreateTodoAsync(todoDto);
            return CreatedAtAction(nameof(GetTodosById), new { todoGuid = todo.Guid }, todo);
        }

        /// <summary>
        /// Update Todo data
        /// </summary>
        /// <param name="todoGuid">Todo Guid for comparision</param>
        /// <param name="todoDtoPut">Todo DTO to update data</param>
        /// <returns></returns>
        [HttpPut("{todoGuid:Guid}")]
        public async Task<ActionResult<TodoDtoGet>> UpdateTodo([FromRoute] Guid todoGuid,
            [FromBody] TodoDtoPut todoDtoPut)
        {
            return Ok(await _service.UpdateTodoAsync(todoGuid, todoDtoPut));
        }

        /// <summary>
        /// Update todo status
        /// </summary>
        /// <param name="todoGuid">Todo Guid for comparision</param>
        /// <param name="status">New Status to store</param>
        /// <returns></returns>
        [HttpPatch("{todoGuid:Guid}/{status}")]
        public async Task<ActionResult<TodoDtoGet>> UpdateTodoStatus([FromRoute] Guid todoGuid,
            [FromRoute] StatusDto status)
        {
            return Ok(await _service.UpdateTodoStatusAsync(todoGuid, status));
        }

        /// <summary>
        /// Soft delete todo
        /// </summary>
        /// <param name="todoGuid">Todo Guid for comparision</param>
        /// <param name="HardDelete">To force database delete</param>
        /// <returns></returns>
        [HttpDelete("{todoGuid:Guid}")]
        public ActionResult DeleteTodo([FromRoute] Guid todoGuid, [FromQuery] bool HardDelete)
        {
            _service.DeleteTodo(todoGuid, HardDelete);
            return NoContent();
        }
    }
}