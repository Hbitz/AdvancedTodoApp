using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Common
{
    public enum ErrorCode
    {
        // TODO - Create grouping to better reflect area, e.g 1-99 domain errors, 100-199 something else etc?
        None = 0,           // No error
        NotFound = 1,       // Resource not found
        Unauthorized = 2,   // User don't have access
        ValidationError = 3,// Input validation failed
        Conflict = 4,       // Duplicate/conflicting resource
        UnknownError = 100  // Catch-all for uncategorized errors
    }
}
