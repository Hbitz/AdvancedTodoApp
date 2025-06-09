using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Domain.Common;

namespace AdvancedTodoApp.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string ?Description { get; set; }
        public bool IsCompleted { get; set; }

        // Relation to user
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Relation to category(optional, as Todos don't require a category to exist)
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
