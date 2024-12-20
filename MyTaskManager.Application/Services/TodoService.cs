using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todo?> GetByIdAsync(Guid id, Guid userId)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null || todo.UserId != userId)
            {
                return null;
            }

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllByUserIdAsync(Guid userId)
        {
            return await _todoRepository.GetAllByUserIdAsync(userId);
        }

        public async Task AddAsync(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreatedAt = DateTime.UtcNow;

            await _todoRepository.AddAsync(todo);
        }

        public async Task UpdateAsync(Todo todo, Guid userId)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(todo.Id);

            if (existingTodo == null || existingTodo.UserId != userId)
            {
                throw new UnauthorizedAccessException("You cannot update this task.");
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.CompletedAt = DateTime.UtcNow;

            await _todoRepository.UpdateAsync(existingTodo);
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null || todo.UserId != userId)
            {
                throw new UnauthorizedAccessException("You cannot delete this task.");
            }

            await _todoRepository.DeleteAsync(id);
        }
    }
}
