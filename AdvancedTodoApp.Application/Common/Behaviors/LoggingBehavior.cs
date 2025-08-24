using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using AdvancedTodoApp.Application.Common.Models;
using AdvancedTodoApp.Application.Common.Helpers;

namespace AdvancedTodoApp.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        // If the duration of a request is over threshold, log with a warning
        private readonly int _warningThreshholdMilliseconds = 1000;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Get the name of the request
            var requestName = typeof(TRequest).Name;
            // Use Helper class to seralize data and hide/redact the sensitive fields such as password etc.
            var requestData = LoggingHelper.RedactSensitiveData(request);

            // Log the request and then start a timer
            _logger.LogInformation("Handling {RequestName} with data: {RequestData}", requestName, requestData);
            var stopwatch = Stopwatch.StartNew();
            try
            {
                // Call the next handler in the pipeline
                var response = await next();

                // stop timer and log info
                stopwatch.Stop();

                // If response is OperationResult and failed, we still want to log the request - but with a warning.
                if (response is IOperationResult op && !op.Success)
                {
                    _logger.LogWarning("Request {RequestName} completed with validation or domain errors: {Errors} (took {ElapsedMilliseconds}ms)",
                        requestName,
                        string.Join(", ", op.Errors),
                        stopwatch.ElapsedMilliseconds);
                }
                // Else log normally, unless duration of a request is deemed too long - in which case we log as a warning as well.
                else
                {
                    
                    if (stopwatch.ElapsedMilliseconds > _warningThreshholdMilliseconds)
                    {
                        _logger.LogWarning("Request {RequestName} took {ElapsedMilliseconds}ms which exceeds threshold",
                            requestName,
                            stopwatch.ElapsedMilliseconds);
                    }
                    // Normal logging
                    else
                    {
                        _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms",
                            requestName,
                            stopwatch.ElapsedMilliseconds);
                    }
                }

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
