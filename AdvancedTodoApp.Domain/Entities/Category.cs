using AdvancedTodoApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        // Reminder: "= null!" tells compiler we know this is Non-Nullable, but will be initialized later outside constructor, so we dont need warnings.
        // We initialize this via EFC so no worries.
        public string Name { get; set; } = null!; 
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
