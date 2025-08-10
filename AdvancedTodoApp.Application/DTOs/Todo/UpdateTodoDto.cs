using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.DTOs.Todo
{
    public class UpdateTodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
