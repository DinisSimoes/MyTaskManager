using Moq;
using MyTaskManager.Application.Services;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;

namespace MyTaskManager.Test.Services
{
    public class TodoServiceTests
    {
        private Mock<ITodoRepository> _todoRepositoryMock;
        private TodoService _todoService;

        public TodoServiceTests()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _todoService = new TodoService(_todoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTodo_WhenTodoExistsAndBelongsToUser()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var todo = new Todo { Id = todoId, UserId = userId };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId))
                .ReturnsAsync(todo);

            // Act
            var result = await _todoService.GetByIdAsync(todoId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoId, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        [Theory]
        [InlineData("12345678-1234-1234-1234-123456789012", "87654321-4321-4321-4321-210987654321")]
        [InlineData("12345678-1234-1234-1234-123456789012", "12345678-1234-1234-1234-000000000000")]
        public async Task GetByIdAsync_ReturnsNull_WhenTodoDoesNotBelongToUser(string todoIdStr, string userIdStr)
        {
            // Arrange
            var todoId = Guid.Parse(todoIdStr);
            var userId = Guid.Parse(userIdStr);
            var todo = new Todo { Id = todoId, UserId = Guid.NewGuid() }; // Different UserId

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId))
                .ReturnsAsync(todo);

            // Act
            var result = await _todoService.GetByIdAsync(todoId, userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_ReturnsTodos_ForGivenUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var todos = new List<Todo>
            {
                new Todo { Id = Guid.NewGuid(), UserId = userId },
                new Todo { Id = Guid.NewGuid(), UserId = userId }
            };

            _todoRepositoryMock.Setup(repo => repo.GetAllByUserIdAsync(userId))
                .ReturnsAsync(todos);

            // Act
            var result = await _todoService.GetAllByUserIdAsync(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(userId, t.UserId));
        }

        [Fact]
        public async Task AddAsync_SetsIdAndCreatedAt_BeforeAdding()
        {
            // Arrange
            var todo = new Todo { Title = "Test", Description = "Test description" };

            _todoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Todo>()))
                .Returns(Task.CompletedTask);

            // Act
            await _todoService.AddAsync(todo);

            // Assert
            Assert.NotEqual(Guid.Empty, todo.Id);
            Assert.True(todo.CreatedAt > DateTime.MinValue);
            _todoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Todo>()), Times.Once);
        }

        [Theory]
        [InlineData("12345678-1234-1234-1234-123456789012", "87654321-4321-4321-4321-210987654321")]
        public async Task UpdateAsync_ThrowsUnauthorizedAccessException_WhenTodoDoesNotBelongToUser(string todoIdStr, string userIdStr)
        {
            // Arrange
            var todoId = Guid.Parse(todoIdStr);
            var userId = Guid.Parse(userIdStr);
            var todo = new Todo { Id = todoId, Title = "Updated Title" };
            var existingTodo = new Todo { Id = todoId, UserId = Guid.NewGuid() };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId))
                .ReturnsAsync(existingTodo);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _todoService.UpdateAsync(todo, userId));
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTodo_WhenTodoBelongsToUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var todo = new Todo { Id = Guid.NewGuid(), Title = "Updated Title", Description = "Updated Description" };
            var existingTodo = new Todo { Id = todo.Id, UserId = userId, Title = "Old Title", Description = "Old Description" };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todo.Id))
                .ReturnsAsync(existingTodo);

            // Act
            await _todoService.UpdateAsync(todo, userId);

            // Assert
            _todoRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Todo>(t =>
                t.Title == "Updated Title" &&
                t.Description == "Updated Description"
            )), Times.Once);
        }

        [Theory]
        [InlineData("12345678-1234-1234-1234-123456789012", "87654321-4321-4321-4321-210987654321")]
        public async Task DeleteAsync_ThrowsUnauthorizedAccessException_WhenTodoDoesNotBelongToUser(string todoIdStr, string userIdStr)
        {
            // Arrange
            var todoId = Guid.Parse(todoIdStr);
            var userId = Guid.Parse(userIdStr);
            var existingTodo = new Todo { Id = todoId, UserId = Guid.NewGuid() };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId))
                .ReturnsAsync(existingTodo);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _todoService.DeleteAsync(todoId, userId));
        }

        [Fact]
        public async Task DeleteAsync_DeletesTodo_WhenTodoBelongsToUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var todoId = Guid.NewGuid();
            var existingTodo = new Todo { Id = todoId, UserId = userId };

            _todoRepositoryMock.Setup(repo => repo.GetByIdAsync(todoId))
                .ReturnsAsync(existingTodo);

            // Act
            await _todoService.DeleteAsync(todoId, userId);

            // Assert
            _todoRepositoryMock.Verify(repo => repo.DeleteAsync(todoId), Times.Once);
        }
    }
}
