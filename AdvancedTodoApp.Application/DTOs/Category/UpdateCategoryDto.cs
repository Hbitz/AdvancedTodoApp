using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
