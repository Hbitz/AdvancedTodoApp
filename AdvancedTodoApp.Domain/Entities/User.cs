using AdvancedTodoApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Domain.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// The domain layer is core business logic and focuses on business concepts.
        /// We don't store passwords here as it required to be handled carefully.
        /// Authentication and password management should be handed in the INfrastructure layer or via dedicated Identity systems like ASP.NET Identity.
        /// </summary>
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
