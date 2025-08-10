using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using AdvancedTodoApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Persistence.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<List<TodoItem>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.TodoItems
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public void Add(TodoItem todo)
        {
            _context.TodoItems.Add(todo);
        }

        public void Update(TodoItem todo)
        {
            _context.TodoItems.Update(todo);
        }

        public void Delete(TodoItem todo)
        {
            _context.TodoItems.Remove(todo);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
