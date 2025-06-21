using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using AdvancedTodoApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTodoApp.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
