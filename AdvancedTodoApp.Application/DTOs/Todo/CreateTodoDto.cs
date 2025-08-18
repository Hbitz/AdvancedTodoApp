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
        // TODO: Currently only takes full GUID as ID, a rougher developer experience, might  expand on the feature/functionality of this
        public Guid? CategoryId { get; set; }
    }
}
