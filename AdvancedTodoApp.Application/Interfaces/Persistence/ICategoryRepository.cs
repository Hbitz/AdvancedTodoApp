using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Domain.Entities;
using AdvancedTodoApp.Application.DTOs.Category;

namespace AdvancedTodoApp.Application.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<List<Category>> GetAllByUserIdAsync(Guid userId);
        // Synchronous - just modifies EFC in-memory change tracker. 
        // Could make it so these save on their own, but it would sacrifice control and efficiency of transactions, like if we want to update category and todo at same time.
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        // Save all changes. 
        Task<bool> SaveChangesAsync();
    }
}
