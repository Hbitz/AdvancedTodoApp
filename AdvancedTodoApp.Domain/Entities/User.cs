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
        /// We can store the PasswordHash here as it's used by the AuthService internally but never exposed in DTOs or elsewhere.
        /// And while we store the PasswordHash here, it's important the validation, hasing and user login logic still lives in the applictaion/infrastructure layers
        /// </summary>
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
