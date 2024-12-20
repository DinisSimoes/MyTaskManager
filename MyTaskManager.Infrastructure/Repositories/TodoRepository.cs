using Microsoft.EntityFrameworkCore;
using MyTaskManager.Domain.Entities;
using MyTaskManager.Domain.Interfaces;
using MyTaskManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Todos
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Todo>> GetAllByUserIdAsync(Guid userId)
        {
            return await _dbContext.Todos
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Todo todo)
        {
            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _dbContext.Todos.Update(todo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
