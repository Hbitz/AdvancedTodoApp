using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.DTOs.Todo
{
    public class CreateTodoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool isCompleted { get; set; }
        public Guid UserId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
