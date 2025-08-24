using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace AdvancedTodoApp.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Get the name of the request and serialize the data
            var requestName = typeof(TRequest).Name;
            var requestData = JsonSerializer.Serialize(request);

            // Log the request and then start a timer
            _logger.LogInformation("Handling {RequestName} with data: {RequestData}", requestName, requestData);
            var stopwatch = Stopwatch.StartNew();
            try
            {
                // Call the next handler in the pipeline
                var response = await next();

                // stop timer and log info
                stopwatch.Stop();
                _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms", requestName, stopwatch.ElapsedMilliseconds);

                // If response is of the OperationResult type and fails, we can log warnings here

                return response;
            }
            catch (Exception ex)
            {
                // Stop timer and log details if we run into exception
                _logger.LogError(ex, "Exception handling {RequestName} after {ElapsedMilliseconds}ms", requestName, stopwatch.ElapsedMilliseconds);

                // Re-throw expceiton to let higher layers handle it
                throw;
            }
        }
    }
}
