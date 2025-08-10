using AdvancedTodoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Interfaces.Persistence
{
    public interface ITodoRepository
    {
        void Add(TodoItem todo);
        void Update(TodoItem todo);
        void Delete(TodoItem todo);
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<List<TodoItem>> GetAllByUserIdAsync(Guid id);
        Task<bool>SaveChangesAsync();
    }
}
