using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Todo;
using Application.DTO.Todo.Enums;
using Application.IServices;
using Application.Mapper;
using Application.Services;
using AutoMapper;
using Moq;
using TodoV2.Domain.Entities;
using TodoV2.Infrastructure.IRepository;
using Xunit;

namespace TodoV2.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly ITodoService _todoService;

        public TodoServiceTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profiles>(); // Substitua por seu perfil de mapeamento real
            }).CreateMapper();

            _todoService = new TodoService(_repositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetTodosAsync_Should_ReturnListOfTodos()
        {
            // Arrange
            var todoEntities = new List<Todo>(); // Crie uma lista de entidades simulada
            _repositoryMock.Setup(repo => repo.GetTodosAsync()).ReturnsAsync(todoEntities);

            // Act
            var result = await _todoService.GetTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoEntities.Count, result.Count);
        }

        [Fact]
        public async Task GetTodoByGuidAsync_Should_ReturnTodo()
        {
            // Arrange
            var todoGuid = Guid.NewGuid();
            var todoEntity = new Todo(); // Crie uma entidade simulada
            _repositoryMock.Setup(repo => repo.GetTodoByGuidAsync(todoGuid, true)).ReturnsAsync(todoEntity);

            // Act
            var result = await _todoService.GetTodoByGuidAsync(todoGuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoEntity.Guid, result.Guid);
        }

        [Fact]
        public async Task CreateTodoAsync_Should_ReturnCreatedTodo()
        {
            // Arrange
            var newTodoDto = new TodoDtoPost(); // Crie um DTO simulado
            var createdTodoEntity = new Todo(); // Crie uma entidade simulada
            _repositoryMock.Setup(repo => repo.CreateTodoAsync(It.IsAny<Todo>())).ReturnsAsync(createdTodoEntity);

            // Act
            var result = await _todoService.CreateTodoAsync(newTodoDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdTodoEntity.Guid, result.Guid);
        }

        [Fact]
        public async Task UpdateTodoAsync_Should_ReturnUpdatedTodo()
        {
            // Arrange
            var todoGuid = Guid.NewGuid();
            var newTodoDto = new TodoDtoPut(); // Crie um DTO simulado
            var todoEntity = new Todo(); // Crie uma entidade simulada
            _repositoryMock.Setup(repo => repo.GetTodoByGuidAsync(todoGuid, false)).ReturnsAsync(todoEntity);
            _repositoryMock.Setup(repo => repo.UpdateTodoAsync(It.IsAny<Todo>())).ReturnsAsync(todoEntity);

            // Act
            var result = await _todoService.UpdateTodoAsync(todoGuid, newTodoDto);

            // Assert
            Assert.NotNull(result);
            // Faça as verificações necessárias para as propriedades atualizadas do todo retornado
        }

        [Fact]
        public async Task UpdateTodoStatusAsync_Should_ReturnUpdatedTodo()
        {
            // Arrange
            var todoGuid = Guid.NewGuid();
            var statusDto = StatusDto.Wip; // Substitua por um valor de DTO simulado
            var todoEntity = new Todo(); // Crie uma entidade simulada
            _repositoryMock.Setup(repo => repo.GetTodoByGuidAsync(todoGuid, false)).ReturnsAsync(todoEntity);
            _repositoryMock.Setup(repo => repo.UpdateTodoAsync(It.IsAny<Todo>())).ReturnsAsync(todoEntity);

            // Act
            var result = await _todoService.UpdateTodoStatusAsync(todoGuid, statusDto);

            // Assert
            Assert.NotNull(result);
            // Faça as verificações necessárias para a propriedade de status atualizada do todo retornado
        }

        [Fact]
        public async Task DeleteTodo_Should_DeleteTodo()
        {
            // Arrange
            var todoGuid = Guid.NewGuid();
            var todoEntity = new Todo(); // Crie uma entidade simulada
            _repositoryMock.Setup(repo => repo.GetTodoByGuidAsync(todoGuid, true)).ReturnsAsync(todoEntity);

            // Act
            await _todoService.DeleteTodo(todoGuid);

            // Assert
            _repositoryMock.Verify(repo => repo.DeleteTodo(todoEntity), Times.Once);
        }
    }
}