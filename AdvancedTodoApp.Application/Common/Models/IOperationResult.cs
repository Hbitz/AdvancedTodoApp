using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Common.Models
{
    public interface IOperationResult
    {
        bool Success { get; }
        List<string> Errors { get; }
        HttpStatusCode StatusCode { get; }
        ErrorCode ErrorCode { get; }
    }
}
